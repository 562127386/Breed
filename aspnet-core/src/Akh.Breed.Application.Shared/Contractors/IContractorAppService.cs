using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Contractors.Dto;

namespace Akh.Breed.Contractors
{
    public interface IContractorAppService : IApplicationService
    {
        Task<ListResultDto<ContractorListDto>> GetContractors();
    }
}