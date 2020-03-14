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
        
        public PlaqueOfficerAppService(IRepository<PlaqueOfficer> plaqueOfficerRepository, IRepository<Officer> officerRepository, IRepository<PlaqueStore> plaqueStoreRepository)
        {
            _plaqueOfficerRepository = plaqueOfficerRepository;
            _officerRepository = officerRepository;
            _plaqueStoreRepository = plaqueStoreRepository;
        }

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
            
            output.PlaqueStores = _plaqueStoreRepository
                .GetAll().Include(x =>  x.Species)
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.FromCode+"-"+c.ToCode+" "+c.Species.Name))
                .ToList();

            return output;
        }
        
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
        
        public async Task DeletePlaqueOfficer(EntityDto input)
        {
            await _plaqueOfficerRepository.DeleteAsync(input.Id);
        }

        private async Task UpdatePlaqueOfficerAsync(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaqueOfficer = ObjectMapper.Map<PlaqueOfficer>(input);
            await _plaqueOfficerRepository.UpdateAsync(plaqueOfficer);
        }
        
        private async Task CreatePlaqueOfficerAsync(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaqueOfficer = ObjectMapper.Map<PlaqueOfficer>(input);
            await _plaqueOfficerRepository.InsertAsync(plaqueOfficer);
        }
        
        private IQueryable<PlaqueOfficer> GetFilteredQuery(GetPlaqueOfficerInput input)
        {
            long tempSearch = Convert.ToInt64(input.Filter);
            var query = QueryableExtensions.WhereIf(
                _plaqueOfficerRepository.GetAll()
                    .Include(x => x.Officer)
                    .Include(x => x.PlaqueStore)
                    .Include(x => x.FinishedPlaque),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.FromCode <= tempSearch &&
                    u.ToCode >= tempSearch);

            return query;
        }
        
        private async Task CheckValidation(PlaqueOfficerCreateOrUpdateInput input)
        {
            var plaque = _plaqueStoreRepository.GetAll().AsNoTracking().Include(x => x.PlaqueOfficers).FirstOrDefault(x => x.Id == input.PlaqueStoreId);
            input.FromCode = plaque.FromCode;
            if (plaque.PlaqueOfficers.Any())
            {
                // ReSharper disable once PossibleInvalidOperationException
                input.FromCode = plaque.PlaqueOfficers.Max(x => x.ToCode) + 1;
            }
            input.ToCode = input.FromCode + input.PlaqueCount - 1;
            var existingPar = (await _plaqueStoreRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(u => 
                    (u.Id == input.PlaqueStoreId) &&
                    (u.FromCode > input.FromCode ||
                      u.ToCode < input.FromCode ||
                      u.FromCode > input.ToCode ||
                      u.ToCode < input.ToCode)));
            if (existingPar != null && existingPar.Id != input.Id)
            {
                throw new UserFriendlyException(L("RemainInCodeRange", existingPar.ToCode - input.FromCode + 1));
            }
            
            var existingObj = (await _plaqueOfficerRepository.GetAll().AsNoTracking()
                .FirstOrDefaultAsync(u => 
                    (u.Id != input.Id) &&
                    (u.PlaqueStoreId == input.PlaqueStoreId) &&
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