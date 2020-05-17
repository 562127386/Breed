using System.Collections.Generic;
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
using Abp.Zero.Configuration;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.Authorization.Users.Dto;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Officers.Dto;
using Akh.Breed.Unions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Officers
{
    public class OfficerAppService : BreedAppServiceBase, IOfficerAppService
    {
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<AcademicDegree> _academicDegreeRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly RoleManager _roleManager;

        public OfficerAppService(IRepository<Officer> officerRepository, IRepository<AcademicDegree> academicDegreeRepository, IRepository<StateInfo> stateInfoRepository, IRepository<Contractor> contractorRepository, IPasswordHasher<User> passwordHasher, RoleManager roleManager, IRepository<UnionInfo> unionInfoRepository)
        {
            _officerRepository = officerRepository;
            _academicDegreeRepository = academicDegreeRepository;
            _stateInfoRepository = stateInfoRepository;
            _contractorRepository = contractorRepository;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _unionInfoRepository = unionInfoRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer)]
        public async Task<PagedResultDto<OfficerListDto>> GetOfficer(GetOfficerInput input)
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
                query = query.Where(x => x.Contractor.UnionInfoId == union.Id);
            }
            else if (isCityAdmin)
            {
                var contractor = _contractorRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.ContractorId == contractor.Id);
            }
            else
            {
                query = query.Where(x => false);
            }
            var userCount = await query.CountAsync();
            var officers = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var officersListDto = ObjectMapper.Map<List<OfficerListDto>>(officers);
            return new PagedResultDto<OfficerListDto>(
                userCount,
                officersListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer, AppPermissions.Pages_BaseIntro_Officer_Create, AppPermissions.Pages_BaseIntro_Officer_Edit)]
        public async Task<GetOfficerForEditOutput> GetOfficerForEdit(NullableIdDto<int> input)
        {
            Officer officer = null;
            if (input.Id.HasValue)
            {
                officer = await _officerRepository.GetAll()
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new GetOfficerForEditOutput();
            
            //officer
            output.Officer = officer != null
                ? ObjectMapper.Map<OfficerCreateOrUpdateInput>(officer)
                : new OfficerCreateOrUpdateInput();
            
            //AcademicDegrees
            output.AcademicDegrees = _academicDegreeRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.AcademicDegreeId == c.Id })
                .ToList();
            
            //StateInfos
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var isCityAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.CityAdmin);
            var stateInfoQuery = _stateInfoRepository.GetAll();
            if (isAdmin || isSysAdmin)
            {
                stateInfoQuery = stateInfoQuery;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                stateInfoQuery = stateInfoQuery.Where(x => x.Id == union.StateInfoId);
            }
            else if (isCityAdmin)
            {
                var contractor = _contractorRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                stateInfoQuery = stateInfoQuery.Where(x => x.Id == contractor.StateInfoId);
            }
            else
            {
                stateInfoQuery = stateInfoQuery.Where(x => false);
            }
            output.StateInfos = stateInfoQuery
                .ToList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Officer.StateInfoId == c.Id })
                .ToList();
            
            //Contractors
            if (output.Officer.StateInfoId > 0)
            {
                output.Contractors = _contractorRepository
                    .GetAllList()
                    .Where(x => x.StateInfoId == output.Officer.StateInfoId)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" + c.Name + "," + c.Family + ")")
                        {IsSelected = output.Officer.ContractorId == c.Id})
                    .ToList();
            }

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Create, AppPermissions.Pages_BaseIntro_Officer_Edit)]
        public async Task CreateOrUpdateOfficer(OfficerCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateOfficerAsync(input);
            }
            else
            {
                await CreateOfficerAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Delete)]
        public async Task DeleteOfficer(EntityDto input)
        {
            try
            {
                await _officerRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Edit)]
        private async Task UpdateOfficerAsync(OfficerCreateOrUpdateInput input)
        {
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            user.Name = input.Name;
            user.Surname = input.Family;
            user.UserName = input.UserName;
            user.EmailAddress = input.UserName + "@mgnsys.ir";
            CheckErrors(await UserManager.UpdateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();
            
            var officer = ObjectMapper.Map<Officer>(input);
            await _officerRepository.UpdateAsync(officer);
        }
        
        [AbpAuthorize(AppPermissions.Pages_BaseIntro_Officer_Create)]
        private async Task CreateOfficerAsync(OfficerCreateOrUpdateInput input)
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
            var officerRole = _roleManager.GetRoleByName(StaticRoleNames.Host.Officer);
            long userId = user.ToUserIdentifier().UserId;
            user.Roles = new List<UserRole>();
            user.Roles.Add(new UserRole(null, user.Id, officerRole.Id));

            if (userId > 0)
            {
                var officer = ObjectMapper.Map<Officer>(input);
                officer.UserId = userId;
                await _officerRepository.InsertAsync(officer);
            }
            else
            {
                throw new UserFriendlyException(L("AnErrorOccurred"));
            }
        }
        
        public List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input)
        {
           var query = _contractorRepository.GetAll();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.StateInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(), c.FirmName + " (" + c.Name + "," + c.Family + ")"))
                .ToList();
        }
        
        private IQueryable<Officer> GetFilteredQuery(GetOfficerInput input)
        {
            var query = QueryableExtensions.WhereIf(_officerRepository.GetAll()
                    .Include(x => x.Contractor)
                    .Include(x => x.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Family.Contains(input.Filter) ||
                    u.NationalCode.Replace("-","").Contains(input.Filter) ||
                    u.MobileNumber.Replace("-","").Contains(input.Filter) ||
                    u.Contractor.Name.Contains(input.Filter) ||
                    u.Contractor.Family.Contains(input.Filter) ||
                    u.Contractor.FirmName.Contains(input.Filter) ||
                    u.StateInfo.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}