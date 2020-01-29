using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Akh.Breed.Contractors.Dto;

namespace Akh.Breed.Contractors
{
    public class ContractorAppService : BreedAppServiceBase, IContractorAppService
    {
        private readonly IRepository<Contractor, long> _contractorRepository;

        public ContractorAppService(
            IRepository<Contractor, long> contractorRepository)
        {
            _contractorRepository = contractorRepository;
        }
        
        public async Task<ListResultDto<ContractorListDto>> GetContractors()
        {
            var contractors = await _contractorRepository.GetAllListAsync();
            var result = new List<ContractorListDto>();
            
            foreach (var contractor in contractors)
            {
                var resultContractor = ObjectMapper.Map<ContractorListDto>(contractor);

                result.Add(resultContractor);
            }

            return new ListResultDto<ContractorListDto>(result);

        }
    }
}