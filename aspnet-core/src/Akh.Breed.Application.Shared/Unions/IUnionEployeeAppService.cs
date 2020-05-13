using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Unions.Dto;

namespace Akh.Breed.Unions
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