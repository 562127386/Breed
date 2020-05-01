using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Plaques.Dto;
using Akh.Breed.Officers;
using Akh.Breed.Plaques;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueToHerdAppService : BreedAppServiceBase, IPlaqueToHerdAppService
    {
        private readonly IRepository<PlaqueToHerd> _plaqueToHerdRepository;
        private readonly IRepository<Herd> _herdRepository;
        private readonly IRepository<PlaqueToOfficer> _plaqueToOfficerRepository;
        private readonly IRepository<PlaqueInfo, long> _plaqueInfoRepository;
        private readonly IRepository<Officer> _officerRepository;
       
        
        public PlaqueToHerdAppService(IRepository<PlaqueToHerd> plaqueToHerdRepository, IRepository<Herd> herdRepository, IRepository<PlaqueToOfficer> plaqueToOfficerRepository, IRepository<PlaqueInfo, long> plaqueInfoRepository, IRepository<Officer> officerRepository)
        {
            _plaqueToHerdRepository = plaqueToHerdRepository;
            _herdRepository = herdRepository;
            _plaqueToOfficerRepository = plaqueToOfficerRepository;
            _plaqueInfoRepository = plaqueInfoRepository;
            _officerRepository = officerRepository;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd)]
        public async Task<PagedResultDto<PlaqueToHerdListDto>> GetPlaqueToHerd(GetPlaqueToHerdInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueToHerds = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueToHerdsListDto = ObjectMapper.Map<List<PlaqueToHerdListDto>>(plaqueToHerds);
            return new PagedResultDto<PlaqueToHerdListDto>(
                userCount,
                plaqueToHerdsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Create, AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Edit)]
        public async Task<PlaqueToHerdGetForEditOutput> GetPlaqueToHerdForEdit(NullableIdDto<int> input)
        {
            var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (officer == null)
            {
                throw new UserFriendlyException(L("TheOfficerDoesNotExists"));
            }
            PlaqueToHerd plaqueToHerd = null;
            if (input.Id.HasValue)
            {
                plaqueToHerd = await _plaqueToHerdRepository.GetAll()
                    .Include(x => x.Officer)
                    .FirstOrDefaultAsync(x => x.Id == input.Id.Value);
            }
            
            var output = new PlaqueToHerdGetForEditOutput();
            
            //plaqueToHerd
            var newLiveStock = new PlaqueToHerdCreateOrUpdateInput();
            //newLiveStock.CreationTime = newLiveStock.CreationTime.GetShamsi();
            output.PlaqueToHerd = plaqueToHerd != null
                ? ObjectMapper.Map<PlaqueToHerdCreateOrUpdateInput>(plaqueToHerd)
                : newLiveStock;
            
            output.Herds = _herdRepository
                .GetAllList()
                .Where(x => x.CreatorUserId ==  AbpSession.UserId)
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Code + " - " + c.HerdName + "(" +c.Name+","+c.Family+")" ))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Create, AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Edit)]
        public async Task CreateOrUpdatePlaqueToHerd(PlaqueToHerdCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueToHerdAsync(input);
            }
            else
            {
                await CreatePlaqueToHerdAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Delete)]
        public async Task DeletePlaqueToHerd(EntityDto input)
        {
            try
            {
                await _plaqueToHerdRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Edit)]
        private async Task UpdatePlaqueToHerdAsync(PlaqueToHerdCreateOrUpdateInput input)
        {
            var plaqueToHerd = ObjectMapper.Map<PlaqueToHerd>(input);
            await _plaqueToHerdRepository.UpdateAsync(plaqueToHerd);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToHerd_Create)]
        private async Task CreatePlaqueToHerdAsync(PlaqueToHerdCreateOrUpdateInput input)
        {
            var plaqueToHerd = ObjectMapper.Map<PlaqueToHerd>(input);
            await _plaqueToHerdRepository.InsertAsync(plaqueToHerd);
            
            var plaqueInfo = new PlaqueInfo
            {
                Code =  Convert.ToInt64(plaqueToHerd.NationalCode),
                Latitude = plaqueToHerd.Latitude,
                Longitude = plaqueToHerd.Longitude,
                OfficerId = plaqueToHerd.OfficerId,
                StateId = 5
            };
            await _plaqueInfoRepository.InsertAsync(plaqueInfo);
        }
        
        private IQueryable<PlaqueToHerd> GetFilteredQuery(GetPlaqueToHerdInput input)
        {
            var tUser = UserManager.GetUserById(AbpSession.GetUserId());
            var isOfficer = UserManager.IsInRoleAsync(tUser,StaticRoleNames.Host.Officer).Result;
            
            var query = QueryableExtensions.WhereIf(
                _plaqueToHerdRepository.GetAll()
                .Include(x => x.Herd)
                .Include(x => x.Officer),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.NationalCode.Contains(input.Filter));

            if (isOfficer)
            {
                query = query.Where(x => x.CreatorUserId == AbpSession.UserId);
            }
            
            return query;
        }        
        
        public async Task<PlaqueToHerdCreateOrUpdateInput> CheckValidation(PlaqueToHerdCreateOrUpdateInput input)
        {
            long FromCode = 364052000000000;
            long ToCode = 364052999999999;
            long nationalCode = Convert.ToInt64(input.NationalCode);
            if ( nationalCode < FromCode)
            {
                nationalCode += FromCode;
            }
            if ( nationalCode < FromCode || nationalCode > ToCode)
            {
                throw new UserFriendlyException(L("ThisCodeRangeShouldBe"));
            }

            var plaqueInfo = _plaqueInfoRepository.FirstOrDefault(x => x.Code == nationalCode);
            if ( plaqueInfo != null)
            {
                throw new UserFriendlyException(L("ThisCodeIsAllocated",nationalCode));
            }
            
            var officer = _officerRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
            if (officer == null)
            {
                throw new UserFriendlyException(L("TheOfficerDoesNotExists"));
            }
            else
            {
                input.OfficerId = officer.Id;
                var plaqueToOfficer = _plaqueToOfficerRepository.FirstOrDefault(x =>x.FromCode <= nationalCode && x.ToCode >= nationalCode);
                if (plaqueToOfficer == null)
                {
                    throw new UserFriendlyException(L("ThisStoreIsNotAllocatedTo", nationalCode));
                }
            }



            input.NationalCode = nationalCode.ToString();
            return input;
        }
    }
}