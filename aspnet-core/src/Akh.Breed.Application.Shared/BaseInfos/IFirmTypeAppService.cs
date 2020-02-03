using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IFirmTypeAppService : IApplicationService
    {
        Task<PagedResultDto<FirmTypeListDto>> GetFirmType(GetFirmTypeInput input);
        Task<FirmTypeCreateOrUpdateInput> GetFirmTypeForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateFirmType(FirmTypeCreateOrUpdateInput input);
        Task DeleteFirmType(EntityDto input);
    }
}