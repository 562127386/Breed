using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IAnomalyInfoAppService : IApplicationService
    {
        Task<PagedResultDto<AnomalyInfoListDto>> GetAnomalyInfo(GetAnomalyInfoInput input);
        Task<AnomalyInfoCreateOrUpdateInput> GetAnomalyInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateAnomalyInfo(AnomalyInfoCreateOrUpdateInput input);
        Task DeleteAnomalyInfo(EntityDto input);
    }
}