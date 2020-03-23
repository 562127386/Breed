using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Livestocks.Dto;

namespace Akh.Breed.Livestocks
{
    public interface ILivestockAppService : IApplicationService
    {
        Task<PagedResultDto<LivestockListDto>> GetLivestock(GetLivestockInput input);
        Task<GetLivestockForEditOutput> GetLivestockForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateLivestock(LivestockCreateOrUpdateInput input);
        Task DeleteLivestock(EntityDto input);
    }
}