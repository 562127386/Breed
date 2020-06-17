using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface ISpotConnectorInfoAppService : IApplicationService
    {
        Task<PagedResultDto<SpotConnectorInfoListDto>> GetSpotConnectorInfo(GetSpotConnectorInfoInput input);
        Task<SpotConnectorInfoCreateOrUpdateInput> GetSpotConnectorInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateSpotConnectorInfo(SpotConnectorInfoCreateOrUpdateInput input);
        Task DeleteSpotConnectorInfo(EntityDto input);
    }
}