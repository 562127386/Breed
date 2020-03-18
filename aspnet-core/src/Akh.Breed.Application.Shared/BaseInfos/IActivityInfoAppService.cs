using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IActivityInfoAppService : IApplicationService
    {
        Task<PagedResultDto<ActivityInfoListDto>> GetActivityInfo(GetActivityInfoInput input);
        Task<ActivityInfoCreateOrUpdateInput> GetActivityInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateActivityInfo(ActivityInfoCreateOrUpdateInput input);
        Task DeleteActivityInfo(EntityDto input);
    }
}