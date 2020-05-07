using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Officers.Dto;

namespace Akh.Breed.Officers
{
    public interface IOfficerAppService : IApplicationService
    {
        Task<PagedResultDto<OfficerListDto>> GetOfficer(GetOfficerInput input);
        Task<GetOfficerForEditOutput> GetOfficerForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateOfficer(OfficerCreateOrUpdateInput input);
        Task DeleteOfficer(EntityDto input);
        List<ComboboxItemDto> GetForCombo(NullableIdDto<int> input);
    }
}