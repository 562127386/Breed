using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IInseminationAppService : IApplicationService
    {
        Task<PagedResultDto<InseminationListDto>> GetInsemination(GetInseminationInput input);
        Task<GetInseminationForEditOutput> GetInseminationForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateInsemination(InseminationCreateOrUpdateInput input);
        Task<InseminationCreateOrUpdateInput> CheckValidation(InseminationCreateOrUpdateInput input);
        Task DeleteInsemination(EntityDto input);
        List<ComboboxItemDto> GetActivityForCombo(NullableIdDto<int> input);
    }
}