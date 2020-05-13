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
    public class PlaqueToStateAppService :  BreedAppServiceBase, IPlaqueToStateAppService
    {
        private readonly IRepository<PlaqueToState> _plaqueToStateRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<PlaqueStore> _plaqueStoreRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        private readonly IRepository<UnionInfo> _unionInfoRepository;
        private readonly IRepository<Contractor> _contractorRepository;
        
        public PlaqueToStateAppService(IRepository<PlaqueToState> plaqueToStateRepository, IRepository<StateInfo> stateInfoRepository, IRepository<PlaqueStore> plaqueStoreRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<UnionInfo> unionInfoRepository, IRepository<Contractor> contractorRepository)
        {
            _plaqueToStateRepository = plaqueToStateRepository;
            _stateInfoRepository = stateInfoRepository;
            _plaqueStoreRepository = plaqueStoreRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _unionInfoRepository = unionInfoRepository;
            _contractorRepository = contractorRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState)]
        public async Task<PagedResultDto<PlaqueToStateListDto>> GetPlaqueToState(GetPlaqueToStateInput input)
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
                query = query.Where(x => x.StateInfoId == union.StateInfoId);
            }
            else
            {
                query = query.Where(x => false);
            }
            var userCount = await query.CountAsync();
            var plaqueToStates = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueToStatesListDto = ObjectMapper.Map<List<PlaqueToStateListDto>>(plaqueToStates);
            return new PagedResultDto<PlaqueToStateListDto>(
                userCount,
                plaqueToStatesListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState_Create, AppPermissions.Pages_IdentityInfo_PlaqueToState_Edit)]
        public async Task<PlaqueToStateGetForEditOutput> GetPlaqueToStateForEdit(NullableIdDto<int> input)
        {
            PlaqueToState plaqueToState = null;
            if (input.Id.HasValue)
            {
                plaqueToState = await _plaqueToStateRepository
                    .GetAll()
                    .Include(x => x.PlaqueStore)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueToStateGetForEditOutput();
            
            //plaqueToState
            var newPlaqueToState = new PlaqueToStateCreateOrUpdateInput();
            newPlaqueToState.SetTime = newPlaqueToState.SetTime.GetShamsi();
            output.PlaqueToState = plaqueToState != null
                ? ObjectMapper.Map<PlaqueToStateCreateOrUpdateInput>(plaqueToState)
                : newPlaqueToState;
            
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
            
            
            output.SpeciesInfos = _speciesInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState_Create, AppPermissions.Pages_IdentityInfo_PlaqueToState_Edit)]
        public async Task CreateOrUpdatePlaqueToState(PlaqueToStateCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueToStateAsync(input);
            }
            else
            {
                await CreatePlaqueToStateAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState_Delete)]
        public async Task DeletePlaqueToState(EntityDto input)
        {
            throw new UserFriendlyException(L("AreYouSureToDeleteThePlaqueState"));
            //await _plaqueToStateRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState_Edit)]
        private async Task UpdatePlaqueToStateAsync(PlaqueToStateCreateOrUpdateInput input)
        {
            var plaqueToState = ObjectMapper.Map<PlaqueToState>(input);
            await _plaqueToStateRepository.UpdateAsync(plaqueToState);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToState_Create)]
        private async Task CreatePlaqueToStateAsync(PlaqueToStateCreateOrUpdateInput input)
        {
            var plaqueStore = _plaqueStoreRepository.Get(input.PlaqueStoreId.Value);
            plaqueStore.LastCode = input.ToCode;
            var plaqueToState = ObjectMapper.Map<PlaqueToState>(input);
            await _plaqueStoreRepository.UpdateAsync(plaqueStore);
            await _plaqueToStateRepository.InsertAsync(plaqueToState);
        }
        
        private IQueryable<PlaqueToState> GetFilteredQuery(GetPlaqueToStateInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueToStateRepository.GetAll()
                    .Include(x => x.StateInfo)
                    .Include(x => x.PlaqueStore)
                    .ThenInclude(x => x.Species),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }

        private async Task CheckValidation(PlaqueToStateCreateOrUpdateInput input)
        {
            if (input.PlaqueCount <= 0)
            {
                throw new UserFriendlyException(L("ThisCodeRangeHasOverlap"));
            }
            var plaqueStoreQuery = _plaqueStoreRepository.GetAll().AsNoTracking()
                .Where(x => x.SpeciesId == input.SpeciesInfoId && x.ToCode != x.LastCode);
            var plaqueStore = await plaqueStoreQuery.FirstOrDefaultAsync(x => (x.LastCode != 0 && x.ToCode - x.LastCode >= input.PlaqueCount) || (x.LastCode == 0 && x.ToCode - x.FromCode + 1 >= input.PlaqueCount));
            if (plaqueStore != null)
            {
                input.FromCode = plaqueStore.LastCode != 0 ? + plaqueStore.LastCode + 1 : plaqueStore.FromCode;
                input.ToCode = input.FromCode + input.PlaqueCount.Value - 1;
                input.PlaqueStoreId = plaqueStore.Id;
            }
            else
            {
                throw new UserFriendlyException(L("RemainInCodeRange", input.PlaqueCount));
            }
        }
    }
}