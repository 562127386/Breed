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
    public class PlaqueOfficerAppService :  BreedAppServiceBase, IPlaqueOfficerAppService
    {
        private readonly IRepository<PlaqueOfficer> _plaqueOfficerRepository;
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<PlaqueStore> _plaqueStoreRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        
        public PlaqueOfficerAppService(IRepository<PlaqueOfficer> plaqueOfficerRepository, IRepository<Officer> officerRepository, IRepository<PlaqueStore> plaqueStoreRepository, IRepository<SpeciesInfo> speciesInfoRepository)
        {
            _plaqueOfficerRepository = plaqueOfficerRepository;
            _officerRepository = officerRepository;
            _plaqueStoreRepository = plaqueStoreRepository;
            _speciesInfoRepository = speciesInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer)]
        public async Task<PagedResultDto<PlaqueOfficerListDto>> GetPlaqueOfficer(GetPlaqueOfficerInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueOfficers = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueOfficersListDto = ObjectMapper.Map<List<PlaqueOfficerListDto>>(plaqueOfficers);
            return new PagedResultDto<PlaqueOfficerListDto>(
                userCount,
                plaqueOfficersListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Create, AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Edit)]
        public async Task<PlaqueOfficerGetForEditOutput> GetPlaqueOfficerForEdit(NullableIdDto<int> input)
        {
            PlaqueOfficer plaqueOfficer = null;
            if (input.Id.HasValue)
            {
                plaqueOfficer = await _plaqueOfficerRepository
                    .GetAll()
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueOfficerGetForEditOutput();
            
            //plaqueOfficer
            output.PlaqueOfficer = plaqueOfficer != null
                ? ObjectMapper.Map<PlaqueOfficerCreateOrUpdateInput>(plaqueOfficer)
                : new PlaqueOfficerCreateOrUpdateInput();
            
            //StateInfos
            output.Officers = _officerRepository
                .GetAll().Include(x => x.Contractor)
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Contractor.FirmName+" "+c.NationalCode+" ("+c.Name+","+c.Family+")"))
                .ToList();
            
            
            output.SpeciesInfos = _speciesInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Create, AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Edit)]
        public async Task CreateOrUpdatePlaqueOfficer(PlaqueOfficerCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueOfficerAsync(input);
            }
            else
            {
                await CreatePlaqueOfficerAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Delete)]
        public async Task DeletePlaqueOfficer(EntityDto input)
        {
            throw new UserFriendlyException(L("AreYouSureToDeleteThePlaqueState"));
            //await _plaqueOfficerRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Edit)]
        private async Task UpdatePlaqueOfficerAsync(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaqueOfficer = ObjectMapper.Map<PlaqueOfficer>(input);
            await _plaqueOfficerRepository.UpdateAsync(plaqueOfficer);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueOfficer_Create)]
        private async Task CreatePlaqueOfficerAsync(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaqueStore = _plaqueStoreRepository.Get(input.PlaqueStoreId.Value);
            plaqueStore.LastCode = input.ToCode;
            var plaqueOfficer = ObjectMapper.Map<PlaqueOfficer>(input);
            await _plaqueStoreRepository.UpdateAsync(plaqueStore);
            await _plaqueOfficerRepository.InsertAsync(plaqueOfficer);
        }
        
        private IQueryable<PlaqueOfficer> GetFilteredQuery(GetPlaqueOfficerInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueOfficerRepository.GetAll()
                    .Include(x => x.Officer)
                    .Include(x => x.PlaqueStore)
                    .ThenInclude(x => x.Species)
                    .Include(x => x.FinishedPlaque),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }

        private async Task CheckValidation(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaqueStoreQuery = _plaqueStoreRepository.GetAll().AsNoTracking()
                .Where(x => x.SpeciesId == input.SpeciesInfoId && x.ToCode != x.LastCode);
            var plaqueStore = await plaqueStoreQuery.FirstOrDefaultAsync(x => (x.LastCode != 0 && x.ToCode - x.LastCode >= input.PlaqueCount) || (x.LastCode == 0 && x.ToCode - x.FromCode + 1 >= input.PlaqueCount));
            if (plaqueStore != null)
            {
                input.FromCode = plaqueStore.LastCode != 0 ? + plaqueStore.LastCode + 1 : plaqueStore.FromCode;
                input.ToCode = input.FromCode + input.PlaqueCount - 1;
                input.PlaqueStoreId = plaqueStore.Id;
            }
            else
            {
                throw new UserFriendlyException(L("RemainInCodeRange", input.PlaqueCount));
            }
        }
    }
}