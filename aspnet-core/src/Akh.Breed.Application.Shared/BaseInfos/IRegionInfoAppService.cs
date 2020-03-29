using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IRegionInfoAppService : IApplicationService
    {
        Task<PagedResultDto<RegionInfoListDto>> GetRegionInfo(GetRegionInfoInput input);
        Task<RegionInfoGetForEditOutput> GetRegionInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateRegionInfo(RegionInfoCreateOrUpdateInput input);
        Task DeleteRegionInfo(EntityDto input);
        List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input);
        string GetCode(NullableIdDto<int> input);
    }
}