using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueToStateAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueToStateListDto>> GetPlaqueToState(GetPlaqueToStateInput input);
        Task<PlaqueToStateGetForEditOutput> GetPlaqueToStateForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueToState(PlaqueToStateCreateOrUpdateInput input);
        Task DeletePlaqueToState(EntityDto input);
    }
}