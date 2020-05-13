using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IUnionInfoAppService : IApplicationService
    {
        Task<PagedResultDto<UnionInfoListDto>> GetUnionInfo(GetUnionInfoInput input);
        Task<UnionInfoGetForEditOutput> GetUnionInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateUnionInfo(UnionInfoCreateOrUpdateInput input);
        Task DeleteUnionInfo(EntityDto input);
        List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input);
    }
}