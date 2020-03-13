using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueOfficerAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueOfficerListDto>> GetPlaqueOfficer(GetPlaqueOfficerInput input);
        Task<PlaqueOfficerGetForEditOutput> GetPlaqueOfficerForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueOfficer(PlaqueOfficerCreateOrUpdateInput input);
        Task DeletePlaqueOfficer(EntityDto input);
    }
}