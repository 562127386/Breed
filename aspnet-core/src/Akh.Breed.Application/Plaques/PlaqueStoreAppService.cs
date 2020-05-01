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
using Abp.UI;
using Akh.Breed.Authorization;
using Akh.Breed.BaseInfo;
using Akh.Breed.Plaques.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueStoreAppService :  BreedAppServiceBase, IPlaqueStoreAppService
    {
        private readonly IRepository<PlaqueStore> _plaqueStoreRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        
        public PlaqueStoreAppService(IRepository<PlaqueStore> plaqueStoreRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<Manufacturer> manufacturerRepository)
        {
            _plaqueStoreRepository = plaqueStoreRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore)]
        public async Task<PagedResultDto<PlaqueStoreListDto>> GetPlaqueStore(GetPlaqueStoreInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueStores = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueStoresListDto = ObjectMapper.Map<List<PlaqueStoreListDto>>(plaqueStores);
            return new PagedResultDto<PlaqueStoreListDto>(
                userCount,
                plaqueStoresListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore_Create, AppPermissions.Pages_IdentityInfo_PlaqueStore_Edit)]
        public async Task<PlaqueStoreGetForEditOutput> GetPlaqueStoreForEdit(NullableIdDto<int> input)
        {
            PlaqueStore plaqueStore = null;
            if (input.Id.HasValue)
            {
                plaqueStore = await _plaqueStoreRepository
                    .GetAll()
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueStoreGetForEditOutput();
            
            //plaqueStore
            var newPlaqueStore = new PlaqueStoreCreateOrUpdateInput();
            newPlaqueStore.SetTime = newPlaqueStore.SetTime.GetShamsi();
            output.PlaqueStore = plaqueStore != null
                ? ObjectMapper.Map<PlaqueStoreCreateOrUpdateInput>(plaqueStore)
                : newPlaqueStore;
            
            //speciesInfo
            output.SpecieInfos = _speciesInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            output.Manufacturers = _manufacturerRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore_Create, AppPermissions.Pages_IdentityInfo_PlaqueStore_Edit)]
        public async Task CreateOrUpdatePlaqueStore(PlaqueStoreCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueStoreAsync(input);
            }
            else
            {
                await CreatePlaqueStoreAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore_Delete)]
        public async Task DeletePlaqueStore(EntityDto input)
        {
            try
            {
                await _plaqueStoreRepository.DeleteAsync(input.Id);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch
            {
                throw new UserFriendlyException(L("YouCanNotDeleteThisRecord"));
            }
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore_Edit)]
        private async Task UpdatePlaqueStoreAsync(PlaqueStoreCreateOrUpdateInput input)
        {
            var plaqueStore = ObjectMapper.Map<PlaqueStore>(input);
            await _plaqueStoreRepository.UpdateAsync(plaqueStore);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueStore_Create)]
        private async Task CreatePlaqueStoreAsync(PlaqueStoreCreateOrUpdateInput input)
        {
            var plaqueStore = ObjectMapper.Map<PlaqueStore>(input);
            await _plaqueStoreRepository.InsertAsync(plaqueStore);
        }
        
        private IQueryable<PlaqueStore> GetFilteredQuery(GetPlaqueStoreInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueStoreRepository.GetAll()
                    .Include(x => x.Species)
                    .Include(x => x.Manufacturer)
                    .Include(x => x.FinishedPlaque),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }
        
        private async Task CheckValidation(PlaqueStoreCreateOrUpdateInput input)
        {
            if (input.FromCode >= input.ToCode)
            {
                throw new UserFriendlyException(L("ThisCodeRangeHasOverlap"));
            }
            var species = _speciesInfoRepository.Get(input.SpeciesId.Value);
            if ( input.FromCode < species.FromCode)
            {
                input.FromCode += species.FromCode;
                input.ToCode += species.FromCode;

            }

            if ( input.FromCode < species.FromCode || input.FromCode > species.ToCode || input.ToCode < species.FromCode || input.ToCode > species.ToCode)
            {
                throw new UserFriendlyException(L("ThisCodeRangeShouldBe", species.Name,species.FromCode, species.ToCode));
            }
            var existingObj = (await _plaqueStoreRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(u => 
                    (u.Id != input.Id) &&
                    ((u.FromCode.CompareTo(input.FromCode) <= 0 &&
                     u.ToCode.CompareTo(input.FromCode) >= 0) ||
                    (u.FromCode.CompareTo(input.ToCode) <= 0 &&
                     u.ToCode.CompareTo(input.ToCode) >= 0))));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeRangeHasOverlap",existingObj.FromCode, existingObj.ToCode));
            }
            
        }
    }
}