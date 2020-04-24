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
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.Officers;
using Akh.Breed.Plaques.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueToCityAppService :  BreedAppServiceBase, IPlaqueToCityAppService
    {
        private readonly IRepository<PlaqueToCity> _plaqueToCityRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<PlaqueToState> _plaqueToStateRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        
        public PlaqueToCityAppService(IRepository<PlaqueToCity> plaqueToCityRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository, IRepository<PlaqueToState> plaqueToStateRepository, IRepository<SpeciesInfo> speciesInfoRepository)
        {
            _plaqueToCityRepository = plaqueToCityRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
            _plaqueToStateRepository = plaqueToStateRepository;
            _speciesInfoRepository = speciesInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity)]
        public async Task<PagedResultDto<PlaqueToCityListDto>> GetPlaqueToCity(GetPlaqueToCityInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueToCitys = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueToCitysListDto = ObjectMapper.Map<List<PlaqueToCityListDto>>(plaqueToCitys);
            return new PagedResultDto<PlaqueToCityListDto>(
                userCount,
                plaqueToCitysListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Create, AppPermissions.Pages_IdentityInfo_PlaqueToCity_Edit)]
        public async Task<PlaqueToCityGetForEditOutput> GetPlaqueToCityForEdit(NullableIdDto<int> input)
        {
            PlaqueToCity plaqueToCity = null;
            if (input.Id.HasValue)
            {
                plaqueToCity = await _plaqueToCityRepository
                    .GetAll()
                    .Include(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .Include(x => x.CityInfo)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueToCityGetForEditOutput();
            
            //plaqueToCity
            var newPlaqueToCity = new PlaqueToCityCreateOrUpdateInput();
            newPlaqueToCity.SetTime = newPlaqueToCity.SetTime.GetShamsi();
            output.PlaqueToCity = plaqueToCity != null
                ? ObjectMapper.Map<PlaqueToCityCreateOrUpdateInput>(plaqueToCity)
                : newPlaqueToCity;
            
            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            //CityInfos
            if (output.PlaqueToCity.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository
                    .GetAll()
                    .Where(x => x.StateInfoId == output.PlaqueToCity.StateInfoId.Value)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList(); 
            }
            
            output.SpeciesInfos = _speciesInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Create, AppPermissions.Pages_IdentityInfo_PlaqueToCity_Edit)]
        public async Task CreateOrUpdatePlaqueToCity(PlaqueToCityCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueToCityAsync(input);
            }
            else
            {
                await CreatePlaqueToCityAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Delete)]
        public async Task DeletePlaqueToCity(EntityDto input)
        {
            throw new UserFriendlyException(L("AreYouSureToDeleteThePlaqueState"));
            //await _plaqueToCityRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Edit)]
        private async Task UpdatePlaqueToCityAsync(PlaqueToCityCreateOrUpdateInput input)
        {
            var plaqueToCity = ObjectMapper.Map<PlaqueToCity>(input);
            await _plaqueToCityRepository.UpdateAsync(plaqueToCity);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Create)]
        private async Task CreatePlaqueToCityAsync(PlaqueToCityCreateOrUpdateInput input)
        {
            var plaqueToState = _plaqueToStateRepository.Get(input.PlaqueToStateId.Value);
            plaqueToState.LastCode = input.ToCode;
            var plaqueToCity = ObjectMapper.Map<PlaqueToCity>(input);
            await _plaqueToStateRepository.UpdateAsync(plaqueToState);
            await _plaqueToCityRepository.InsertAsync(plaqueToCity);
        }
        
        private IQueryable<PlaqueToCity> GetFilteredQuery(GetPlaqueToCityInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueToCityRepository.GetAll()
                    .Include(x => x.CityInfo)
                    .ThenInclude(x => x.StateInfo)
                    .Include(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .ThenInclude(x => x.Species),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }

        private async Task CheckValidation(PlaqueToCityCreateOrUpdateInput input)
        {
            var plaqueToStateQuery = _plaqueToStateRepository.GetAll().AsNoTracking()
                .Include(x => x.PlaqueStore)
                .Where(x => x.StateInfoId == input.StateInfoId &&x.PlaqueStore.SpeciesId == input.SpeciesInfoId && x.ToCode != x.LastCode);
            var plaqueToState = await plaqueToStateQuery.FirstOrDefaultAsync(x => (x.LastCode != 0 && x.ToCode - x.LastCode >= input.PlaqueCount) || (x.LastCode == 0 && x.ToCode - x.FromCode + 1 >= input.PlaqueCount));
            if (plaqueToState != null)
            {
                input.FromCode = plaqueToState.LastCode != 0 ? + plaqueToState.LastCode + 1 : plaqueToState.FromCode;
                input.ToCode = input.FromCode + input.PlaqueCount - 1;
                input.PlaqueToStateId = plaqueToState.Id;
            }
            else
            {
                throw new UserFriendlyException(L("RemainInCodeRange", input.PlaqueCount));
            }
        }
    }
}