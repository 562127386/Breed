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

        public ContractorAppService(IRepository<Contractor> contractorRepository)
        {
            _contractorRepository = contractorRepository;
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
        
        public async Task<ContractorCreateOrUpdateInput> GetContractorForEdit(NullableIdDto<int> input)
        {
            //Getting all available roles
            var output = new ContractorCreateOrUpdateInput();
            
            if (input.Id.HasValue)
            {
                //Editing an existing user
                var contractor = await _contractorRepository.GetAsync(input.Id.Value);
                if (contractor != null)
                    ObjectMapper.Map<Contractor,ContractorCreateOrUpdateInput>(contractor,output);
            }

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
            var query = QueryableExtensions.WhereIf(_contractorRepository.GetAll(),
                !input.Filter.IsNullOrWhiteSpace(), u =>
                    u.Name.Contains(input.Filter) ||
                    u.Code.Contains(input.Filter));

            return query;
        }        
   }
}