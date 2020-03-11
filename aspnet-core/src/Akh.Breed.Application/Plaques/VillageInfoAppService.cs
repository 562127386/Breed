using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Akh.Breed.BaseInfo;
using Akh.Breed.Plaques.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Plaques
{
    public class PlaqueStoreAppService :  BreedAppServiceBase, IPlaqueStoreAppService
    {
        private readonly IRepository<PlaqueStore> _plaqueStoreRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        
        public PlaqueStoreAppService(IRepository<PlaqueStore> plaqueStoreRepository, IRepository<SpeciesInfo> speciesInfoRepository)
        {
            _plaqueStoreRepository = plaqueStoreRepository;
            _speciesInfoRepository = speciesInfoRepository;
        }

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
            output.PlaqueStore = plaqueStore != null
                ? ObjectMapper.Map<PlaqueStoreCreateOrUpdateInput>(plaqueStore)
                : new PlaqueStoreCreateOrUpdateInput();
            
            //StateInfos
            output.SpecieInfos = _speciesInfoRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
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
        
        public async Task DeletePlaqueStore(EntityDto input)
        {
            await _plaqueStoreRepository.DeleteAsync(input.Id);
        }

        private async Task UpdatePlaqueStoreAsync(PlaqueStoreCreateOrUpdateInput input)
        {
            var plaqueStore = ObjectMapper.Map<PlaqueStore>(input);
            await _plaqueStoreRepository.UpdateAsync(plaqueStore);
        }
        
        private async Task CreatePlaqueStoreAsync(PlaqueStoreCreateOrUpdateInput input)
        {
            var plaqueStore = ObjectMapper.Map<PlaqueStore>(input);
            await _plaqueStoreRepository.InsertAsync(plaqueStore);
        }
        
        private IQueryable<PlaqueStore> GetFilteredQuery(GetPlaqueStoreInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _plaqueStoreRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    String.Compare(u.FromCode, input.Filter, StringComparison.Ordinal) <= 0 &&
                    String.Compare(u.ToCode, input.Filter, StringComparison.Ordinal) >= 0)p;

            return query;
        }
        
        private async Task CheckValidation(PlaqueStoreCreateOrUpdateInput input)
        {
            var existingObj = (await _plaqueStoreRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(l => l.Code == input.Code && l.RegionInfoId == input.RegionInfoId));
            if (existingObj != null && existingObj.Id != input.Id)
            {
                throw new UserFriendlyException(L("ThisCodeAlreadyExists"));
            }
            
        }
    }
}