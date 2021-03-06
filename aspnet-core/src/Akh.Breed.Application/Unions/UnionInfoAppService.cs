﻿using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;
using Akh.Breed.Unions.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Unions
{
    public class UnionInfoAppService :  BreedAppServiceBase, IUnionInfoAppService
    {
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager _roleManager;

        public UnionInfoAppService(IRepository<UnionInfo> unionInfoRepository, IRepository<StateInfo> stateInfoRepository, IPasswordHasher<User> passwordHasher, RoleManager roleManager)
        {
            _unionInfoRepository = unionInfoRepository;
            _stateInfoRepository = stateInfoRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo)]
        public async Task<PagedResultDto<UnionInfoListDto>> GetUnionInfo(GetUnionInfoInput input)
        {
            var query = GetFilteredQuery(input);
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var isCityAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.CityAdmin);
            if (isAdmin || isSysAdmin)
            {
                query = query;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.Id == union.Id);
            }
            else
            {
                query = query.Where(x => false);
            }
            var userCount = await query.CountAsync();
            var unionInfos = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var unionInfosListDto = ObjectMapper.Map<List<UnionInfoListDto>>(unionInfos);
            return new PagedResultDto<UnionInfoListDto>(
                userCount,
                unionInfosListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo, AppPermissions.Pages_BaseIntro_UnionInfo_Create, AppPermissions.Pages_BaseIntro_UnionInfo_Edit)]
        public async Task<UnionInfoGetForEditOutput> GetUnionInfoForEdit(NullableIdDto<int> input)
        {
            UnionInfo unionInfo = null;
            if (input.Id.HasValue)
            {
                unionInfo = await _unionInfoRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            //Getting all available roles
            var output = new UnionInfoGetForEditOutput();
            
            output.UnionInfo = unionInfo != null
                ? ObjectMapper.Map<UnionInfoCreateOrUpdateInput>(unionInfo)
                : new UnionInfoCreateOrUpdateInput();
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name)
                    {IsSelected = output.UnionInfo.StateInfoId.Equals(c.Id)})
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo_Create, AppPermissions.Pages_BaseIntro_UnionInfo_Edit)]
        public async Task CreateOrUpdateUnionInfo(UnionInfoCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdateUnionInfoAsync(input);
            }
            else
            {
                await CreateUnionInfoAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo_Delete)]
        public async Task DeleteUnionInfo(EntityDto input)
        {
            try
            {
                var unionInfo = _unionInfoRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefault(x => x.Id == input.Id);
                await UserManager.DeleteAsync(unionInfo?.User);
                await _unionInfoRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo_Edit)]
        private async Task UpdateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            user.Name = input.Name;
            user.Surname = input.Family;
            user.UserName = input.UserName;
            user.EmailAddress = input.UserName + "@mgnsys.ir";
            CheckErrors(await UserManager.UpdateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var unionInfo = ObjectMapper.Map<UnionInfo>(input);
            await _unionInfoRepository.UpdateAsync(unionInfo);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_UnionInfo_Create)]
        private async Task CreateUnionInfoAsync(UnionInfoCreateOrUpdateInput input)
        {
            var nationalCode = input.NationalCode.Replace("-", "");
            var user = new User
            {
                IsActive = true,
                ShouldChangePasswordOnNextLogin = true,
                UserName = input.UserName,
                EmailAddress = input.UserName + "@mgnsys.ir",
                Name = input.Name,
                Surname = input.Family
            };
            
            user.Password = _passwordHasher.HashPassword(user, nationalCode);
            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            var officerRole = _roleManager.GetRoleByName(StaticRoleNames.Host.StateAdmin);
            long userId = user.ToUserIdentifier().UserId;
            user.Roles = new List<UserRole>();
            user.Roles.Add(new UserRole(null, user.Id, officerRole.Id));

            if (userId > 0)
            {
                var unionInfo = ObjectMapper.Map<UnionInfo>(input);
                unionInfo.UserId = userId;
                await _unionInfoRepository.InsertAsync(unionInfo);
            }
            else
            {
                throw new UserFriendlyException(L("AnErrorOccurred"));
            }
        }
        
        private IQueryable<UnionInfo> GetFilteredQuery(GetUnionInfoInput input)
        {
            var query = QueryableExtensions.WhereIf(_unionInfoRepository.GetAllIncluding(p => p.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.UnionName.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter) ||
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.StateInfo.Name.Contains(input.Filter));

            return query;
        }
        
        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
            var query = _unionInfoRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.StateInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.UnionName))
                .ToList();
        }
        private async Task CheckValidation(UnionInfoCreateOrUpdateInput input)
        {
            var existingObj = (await _unionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
            existingObj = (await _unionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.UnionName == input.UnionName));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisNameAlreadyExists"));
            }
            
            existingObj = (await _unionInfoRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.StateInfoId == input.StateInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisStateAlreadyExists"));
            }
        }
        
    }
}