using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;
using Akh.Breed.Officers;
using Akh.Breed.Plaques.Dto;
using Akh.Breed.Unions;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueToContractorAppService :  BreedAppServiceBase, IPlaqueToContractorAppService
    {
        private readonly IRepository<PlaqueToContractor> _plaqueToContractorRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<PlaqueToState> _plaqueToStateRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        
        public PlaqueToContractorAppService(IRepository<PlaqueToContractor> plaqueToContractorRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<PlaqueToState> plaqueToStateRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<UnionInfo> unionInfoRepository, IRepository<Contractor> contractorRepository)
        {
            _plaqueToContractorRepository = plaqueToContractorRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _plaqueToStateRepository = plaqueToStateRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _contractorRepository = contractorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor)]
        public async Task<PagedResultDto<PlaqueToContractorListDto>> GetPlaqueToContractor(GetPlaqueToContractorInput input)
        {
            var query = GetFilteredQuery(input);
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var isCityAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.CityAdmin);
            var isOfficer = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Officer);
            if (isAdmin || isSysAdmin)
            {
                query = query;
            }
            else if (isStateAdmin)
            {
                var union = _unionInfoRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.Contractor.CityInfo.StateInfoId == union.StateInfoId);
            }
            else if (isCityAdmin)
            {
                var contractor = _contractorRepository.FirstOrDefault(x => x.UserId == AbpSession.UserId);
                query = query.Where(x => x.Contractor.CityInfoId == contractor.CityInfoId);
            }
            else
            {
                query = query.Where(x => false);
            }
            var userCount = await query.CountAsync();
            var plaqueToContractors = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueToContractorsListDto = ObjectMapper.Map<List<PlaqueToContractorListDto>>(plaqueToContractors);
            return new PagedResultDto<PlaqueToContractorListDto>(
                userCount,
                plaqueToContractorsListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Create, AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Edit)]
        public async Task<PlaqueToContractorGetForEditOutput> GetPlaqueToContractorForEdit(NullableIdDto<int> input)
        {
            PlaqueToContractor plaqueToContractor = null;
            if (input.Id.HasValue)
            {
                plaqueToContractor = await _plaqueToContractorRepository
                    .GetAll()
                    .Include(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .Include(x => x.Contractor)
                    .ThenInclude(x => x.CityInfo)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueToContractorGetForEditOutput();
            
            //plaqueToContractor
            var newPlaqueToContractor = new PlaqueToContractorCreateOrUpdateInput();
            newPlaqueToContractor.SetTime = newPlaqueToContractor.SetTime.GetShamsi();
            output.PlaqueToContractor = plaqueToContractor != null
                ? ObjectMapper.Map<PlaqueToContractorCreateOrUpdateInput>(plaqueToContractor)
                : newPlaqueToContractor;
            
            //StateInfos
            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());
            var isAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin);
            var isSysAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.SysAdmin);
            var isStateAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.StateAdmin);
            var isCityAdmin = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.CityAdmin);
            var isOfficer = await UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Officer);
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
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            //ContractorInfos
            if (output.PlaqueToContractor.StateInfoId.HasValue)
            {
                output.Contractors = _contractorRepository
                    .GetAll()
                    .Where(x => x.StateInfoId == output.PlaqueToContractor.StateInfoId.Value)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList(); 
            }
            
            output.SpeciesInfos = _speciesInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Create, AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Edit)]
        public async Task CreateOrUpdatePlaqueToContractor(PlaqueToContractorCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueToContractorAsync(input);
            }
            else
            {
                await CreatePlaqueToContractorAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Delete)]
        public async Task DeletePlaqueToContractor(EntityDto input)
        {
            throw new UserFriendlyException(L("AreYouSureToDeleteThePlaqueState"));
            //await _plaqueToContractorRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Edit)]
        private async Task UpdatePlaqueToContractorAsync(PlaqueToContractorCreateOrUpdateInput input)
        {
            var plaqueToContractor = ObjectMapper.Map<PlaqueToContractor>(input);
            await _plaqueToContractorRepository.UpdateAsync(plaqueToContractor);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToContractor_Create)]
        private async Task CreatePlaqueToContractorAsync(PlaqueToContractorCreateOrUpdateInput input)
        {
            var plaqueToState = _plaqueToStateRepository.Get(input.PlaqueToStateId.Value);
            plaqueToState.LastCode = input.ToCode;
            var plaqueToContractor = ObjectMapper.Map<PlaqueToContractor>(input);
            await _plaqueToStateRepository.UpdateAsync(plaqueToState);
            await _plaqueToContractorRepository.InsertAsync(plaqueToContractor);
        }
        
        private IQueryable<PlaqueToContractor> GetFilteredQuery(GetPlaqueToContractorInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueToContractorRepository.GetAll()
                    .Include(x => x.Contractor)
                    .ThenInclude(x => x.CityInfo)
                    .ThenInclude(x => x.StateInfo)
                    .Include(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .ThenInclude(x => x.Species),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }

        private async Task CheckValidation(PlaqueToContractorCreateOrUpdateInput input)
        {
            if (input.PlaqueCount <= 0)
            {
                throw new UserFriendlyException(L("ThisCodeRangeHasOverlap"));
            }
            var plaqueToStateQuery = _plaqueToStateRepository.GetAll().AsNoTracking()
                .Include(x => x.PlaqueStore)
                .Where(x => x.StateInfoId == input.StateInfoId &&x.PlaqueStore.SpeciesId == input.SpeciesInfoId && x.ToCode != x.LastCode);
            var plaqueToState = await plaqueToStateQuery.FirstOrDefaultAsync(x => (x.LastCode != 0 && x.ToCode - x.LastCode >= input.PlaqueCount) || (x.LastCode == 0 && x.ToCode - x.FromCode + 1 >= input.PlaqueCount));
            if (plaqueToState != null)
            {
                input.FromCode = plaqueToState.LastCode != 0 ? + plaqueToState.LastCode + 1 : plaqueToState.FromCode;
                input.ToCode = input.FromCode + input.PlaqueCount.Value - 1;
                input.PlaqueToStateId = plaqueToState.Id;
            }
            else
            {
                throw new UserFriendlyException(L("RemainInCodeRange", input.PlaqueCount));
            }
        }
    }
}