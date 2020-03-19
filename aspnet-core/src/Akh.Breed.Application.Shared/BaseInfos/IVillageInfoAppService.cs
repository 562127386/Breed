using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IVillageInfoAppService : IApplicationService
    {
        Task<PagedResultDto<VillageInfoListDto>> GetVillageInfo(GetVillageInfoInput input);
        Task<VillageInfoGetForEditOutput> GetVillageInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateVillageInfo(VillageInfoCreateOrUpdateInput input);
        Task DeleteVillageInfo(EntityDto input);
        
        List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input);
        string GetCode(NullableIdDto<int> input);
    }
}