using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Herds.Dto;

namespace Akh.Breed.Herds
{
    public interface IHerdAppService : IApplicationService
    {
        Task<PagedResultDto<HerdListDto>> GetHerd(GetHerdInput input);
        Task<GetHerdForEditOutput> GetHerdForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateHerd(HerdCreateOrUpdateInput input);
        Task DeleteHerd(EntityDto input);

        List<ComboboxItemDto> GetContractorForCombo(NullableIdDto<int> input);
    }
}