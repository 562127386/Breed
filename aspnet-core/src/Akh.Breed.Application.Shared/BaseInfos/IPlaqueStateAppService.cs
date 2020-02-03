using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IPlaqueStateAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueStateListDto>> GetPlaqueState(GetPlaqueStateInput input);
        Task<PlaqueStateCreateOrUpdateInput> GetPlaqueStateForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueState(PlaqueStateCreateOrUpdateInput input);
        Task DeletePlaqueState(EntityDto input);
    }
}