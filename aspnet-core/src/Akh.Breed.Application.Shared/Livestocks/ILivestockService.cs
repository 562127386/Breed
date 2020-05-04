using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Livestocks.Dto;

namespace Akh.Breed.Livestocks
{
    public interface ILivestockAppService : IApplicationService
    {
        Task<PagedResultDto<MonitoringListDto>> GetMonitoring(GetMonitoringInput input);
        Task<PagedResultDto<LivestockListDto>> GetLivestock(GetLivestockInput input);
        Task<GetLivestockForEditOutput> GetLivestockForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateLivestock(LivestockCreateOrUpdateInput input);
        Task<LivestockCreateOrUpdateInput> CheckValidation(LivestockCreateOrUpdateInput input);
        Task DeleteLivestock(EntityDto input);
        List<ComboboxItemDto> GetActivityForCombo(NullableIdDto<int> input);
    }
}