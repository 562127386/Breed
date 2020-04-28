using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueToOfficerAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueToOfficerListDto>> GetPlaqueToOfficer(GetPlaqueToOfficerInput input);
        Task<PlaqueToOfficerGetForEditOutput> GetPlaqueToOfficerForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueToOfficer(PlaqueToOfficerCreateOrUpdateInput input);
        Task DeletePlaqueToOfficer(EntityDto input);
        List<ComboboxItemDto> GetOfficerForCombo(NullableIdDto<int> input);
    }
}