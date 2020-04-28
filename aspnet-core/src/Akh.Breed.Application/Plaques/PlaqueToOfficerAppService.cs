﻿using System;
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
    public class PlaqueToOfficerAppService :  BreedAppServiceBase, IPlaqueToOfficerAppService
    {
        private readonly IRepository<PlaqueToOfficer> _plaqueToOfficerRepository;
        private readonly IRepository<Officer> _officerRepository;
        private readonly IRepository<StateInfo> _stateInfoRepository;
        private readonly IRepository<CityInfo> _cityInfoRepository;
        private readonly IRepository<PlaqueToCity> _plaqueToCityRepository;
        private readonly IRepository<SpeciesInfo> _speciesInfoRepository;
        
        public PlaqueToOfficerAppService(IRepository<PlaqueToOfficer> plaqueToOfficerRepository, IRepository<Officer> officerRepository, IRepository<PlaqueToCity> plaqueToCityRepository, IRepository<SpeciesInfo> speciesInfoRepository, IRepository<StateInfo> stateInfoRepository, IRepository<CityInfo> cityInfoRepository)
        {
            _plaqueToOfficerRepository = plaqueToOfficerRepository;
            _officerRepository = officerRepository;
            _plaqueToCityRepository = plaqueToCityRepository;
            _speciesInfoRepository = speciesInfoRepository;
            _stateInfoRepository = stateInfoRepository;
            _cityInfoRepository = cityInfoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer)]
        public async Task<PagedResultDto<PlaqueToOfficerListDto>> GetPlaqueToOfficer(GetPlaqueToOfficerInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var plaqueToOfficers = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var plaqueToOfficersListDto = ObjectMapper.Map<List<PlaqueToOfficerListDto>>(plaqueToOfficers);
            return new PagedResultDto<PlaqueToOfficerListDto>(
                userCount,
                plaqueToOfficersListDto
            );
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Create, AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Edit)]
        public async Task<PlaqueToOfficerGetForEditOutput> GetPlaqueToOfficerForEdit(NullableIdDto<int> input)
        {
            PlaqueToOfficer plaqueToOfficer = null;
            if (input.Id.HasValue)
            {
                plaqueToOfficer = await _plaqueToOfficerRepository
                    .GetAll()
                    .Include(x => x.PlaqueToCity)
                    .ThenInclude(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .Include(x => x.PlaqueToCity)
                    .ThenInclude(x => x.CityInfo)
                    .ThenInclude(x => x.StateInfo)
                    .Where(x => x.Id == input.Id.Value)
                    .FirstOrDefaultAsync();
            }
            //Getting all available roles
            var output = new PlaqueToOfficerGetForEditOutput();
            
            //plaqueToOfficer
            var newPlaqueToOfficer = new PlaqueToOfficerCreateOrUpdateInput();
            newPlaqueToOfficer.SetTime = newPlaqueToOfficer.SetTime.GetShamsi();
            output.PlaqueToOfficer = plaqueToOfficer != null
                ? ObjectMapper.Map<PlaqueToOfficerCreateOrUpdateInput>(plaqueToOfficer)
                : newPlaqueToOfficer;

            //StateInfos
            output.StateInfos = _stateInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();
            
            //CityInfos
            if (output.PlaqueToOfficer.StateInfoId.HasValue)
            {
                output.CityInfos = _cityInfoRepository
                    .GetAll()
                    .Where(x => x.StateInfoId == output.PlaqueToOfficer.StateInfoId.Value)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                    .ToList(); 
            }

            if (output.PlaqueToOfficer.CityInfoId.HasValue)
            {
                //OfficerInfos
                output.Officers = _officerRepository
                    .GetAll().Include(x => x.Contractor)
                    .Where(x => x.Contractor.CityInfoId == output.PlaqueToOfficer.CityInfoId.Value)
                    .Select(c => new ComboboxItemDto(c.Id.ToString(),
                        c.Contractor.FirmName + " " + c.NationalCode + " (" + c.Name + "," + c.Family + ")"))
                    .ToList();
            }

            output.SpeciesInfos = _speciesInfoRepository
                .GetAll()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name))
                .ToList();

            return output;
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Create, AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Edit)]
        public async Task CreateOrUpdatePlaqueToOfficer(PlaqueToOfficerCreateOrUpdateInput input)
        {
            await CheckValidation(input);
            
            if (input.Id.HasValue)
            {
                await UpdatePlaqueToOfficerAsync(input);
            }
            else
            {
                await CreatePlaqueToOfficerAsync(input);
            }
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Delete)]
        public async Task DeletePlaqueToOfficer(EntityDto input)
        {
            throw new UserFriendlyException(L("AreYouSureToDeleteThePlaqueState"));
            //await _plaqueToOfficerRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Edit)]
        private async Task UpdatePlaqueToOfficerAsync(PlaqueToOfficerCreateOrUpdateInput input)
        {
            var plaqueToOfficer = ObjectMapper.Map<PlaqueToOfficer>(input);
            await _plaqueToOfficerRepository.UpdateAsync(plaqueToOfficer);
        }
        
        [AbpAuthorize(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Create)]
        private async Task CreatePlaqueToOfficerAsync(PlaqueToOfficerCreateOrUpdateInput input)
        {
            var plaqueToCity = _plaqueToCityRepository.Get(input.PlaqueToCityId.Value);
            plaqueToCity.LastCode = input.ToCode;
            var plaqueToOfficer = ObjectMapper.Map<PlaqueToOfficer>(input);
            await _plaqueToCityRepository.UpdateAsync(plaqueToCity);
            await _plaqueToOfficerRepository.InsertAsync(plaqueToOfficer);
        }
        
        public List<ComboboxItemDto> GetOfficerForCombo(NullableIdDto<int> input)
        {
            var query = _officerRepository
                .GetAll().Include(x => x.Contractor).AsQueryable();
            if (input.Id.HasValue)
            {
                query = query.Where(x => x.Contractor.CityInfoId == input.Id);
            }
            
            return query.Select(c => new ComboboxItemDto(c.Id.ToString(),
                    c.Contractor.FirmName + " " + c.NationalCode + " (" + c.Name + "," + c.Family + ")"))
                .ToList();
        }
        
        private IQueryable<PlaqueToOfficer> GetFilteredQuery(GetPlaqueToOfficerInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueToOfficerRepository.GetAll()
                    .Include(x => x.Officer)
                    .Include(x => x.PlaqueToCity)
                    .ThenInclude(x => x.PlaqueToState)
                    .ThenInclude(x => x.PlaqueStore)
                    .ThenInclude(x => x.Species)
                    .Include(x => x.PlaqueToCity)
                    .ThenInclude(x => x.CityInfo)
                    .ThenInclude(x => x.StateInfo),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }

        private async Task CheckValidation(PlaqueToOfficerCreateOrUpdateInput input)
        {
            if (input.PlaqueCount <= 0)
            {
                throw new UserFriendlyException(L("ThisCodeRangeHasOverlap"));
            }
            var plaqueToCityQuery = _plaqueToCityRepository.GetAll().AsNoTracking()
                .Include(x => x.PlaqueToState)
                .ThenInclude(x => x.PlaqueStore)
                .Where(x => x.CityInfoId == input.CityInfoId && x.PlaqueToState.PlaqueStore.SpeciesId == input.SpeciesInfoId && x.ToCode != x.LastCode);
            var plaqueToCity = await plaqueToCityQuery.FirstOrDefaultAsync(x => (x.LastCode != 0 && x.ToCode - x.LastCode >= input.PlaqueCount) || (x.LastCode == 0 && x.ToCode - x.FromCode + 1 >= input.PlaqueCount));
            if (plaqueToCity != null)
            {
                input.FromCode = plaqueToCity.LastCode != 0 ? + plaqueToCity.LastCode + 1 : plaqueToCity.FromCode;
                input.ToCode = input.FromCode + input.PlaqueCount - 1;
                input.PlaqueToCityId = plaqueToCity.Id;
            }
            else
            {
                throw new UserFriendlyException(L("RemainInCodeRange", input.PlaqueCount));
            }
        }
    }
}