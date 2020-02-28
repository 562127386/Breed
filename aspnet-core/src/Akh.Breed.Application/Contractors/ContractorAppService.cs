using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Akh.Breed.BaseInfo;
using Akh.Breed.BaseInfos.Dto;
using Akh.Breed.Contractors.Dto;
using Microsoft.EntityFrameworkCore;

namespace Akh.Breed.Contractors
{
    public class ContractorAppService : BreedAppServiceBase, IContractorAppService
    {
        private readonly IRepository<Contractor> _contractorRepository;
        private readonly IRepository<FirmType> _firmTypeRepository;

        public ContractorAppService(IRepository<Contractor> contractorRepository, IRepository<FirmType> firmTypeRepository)
        {
            _contractorRepository = contractorRepository;
            _firmTypeRepository = firmTypeRepository;
        }
        public async Task<PagedResultDto<ContractorListDto>> GetContractor(GetContractorInput input)
        {
            var query = GetFilteredQuery(input);
            var userCount = await query.CountAsync();
            var contractors = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var contractorsListDto = ObjectMapper.Map<List<ContractorListDto>>(contractors);
            return new PagedResultDto<ContractorListDto>(
                userCount,
                contractorsListDto
            );
        }
        
        public async Task<GetContractorForEditOutput> GetContractorForEdit(NullableIdDto<int> input)
        {
            Contractor contractor = null;
            if (input.Id.HasValue)
            {
                contractor = await _contractorRepository.GetAsync(input.Id.Value);
            }
            
            var output = new GetContractorForEditOutput();
            
            //contractor
            output.Contractor = contractor != null
                ? ObjectMapper.Map<ContractorEditDto>(contractor)
                : new ContractorEditDto();
            
            //FirmTypes
            output.FirmTypes = _firmTypeRepository
                .GetAllList()
                .Select(c => new ComboboxItemDto(c.Id.ToString(), c.Name ){ IsSelected = output.Contractor.FirmTypeId.Equals(c.Id) })
                .ToList();

            return output;
        }
        
        public async Task CreateOrUpdateContractor(ContractorCreateOrUpdateInput input)
        {
            if (input.Id.HasValue)
            {
                await UpdateContractorAsync(input);
            }
            else
            {
                await CreateContractorAsync(input);
            }
        }
        
        public async Task DeleteContractor(EntityDto input)
        {
            await _contractorRepository.DeleteAsync(input.Id);
        }

        private async Task UpdateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var contractor = ObjectMapper.Map<Contractor>(input);
            await _contractorRepository.UpdateAsync(contractor);
        }
        
        private async Task CreateContractorAsync(ContractorCreateOrUpdateInput input)
        {
            var contractor = ObjectMapper.Map<Contractor>(input);
            await _contractorRepository.InsertAsync(contractor);
        }
        
        private IQueryable<Contractor> GetFilteredQuery(GetContractorInput input)
        {
            var query = QueryableExtensions.WhereIf(
                _contractorRepository.GetAll()
                    .Include(x => x.FirmType),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}