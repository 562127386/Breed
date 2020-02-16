using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IRegionInfoAppService : IApplicationService
    {
        Task<PagedResultDto<RegionInfoListDto>> GetRegionInfo(GetRegionInfoInput input);
        Task<RegionInfoCreateOrUpdateInput> GetRegionInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateRegionInfo(RegionInfoCreateOrUpdateInput input);
        Task DeleteRegionInfo(EntityDto input);
    }
}