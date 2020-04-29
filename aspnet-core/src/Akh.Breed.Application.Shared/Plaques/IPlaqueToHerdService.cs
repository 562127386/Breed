using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueToHerdAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueToHerdListDto>> GetPlaqueToHerd(GetPlaqueToHerdInput input);
        Task<PlaqueToHerdGetForEditOutput> GetPlaqueToHerdForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueToHerd(PlaqueToHerdCreateOrUpdateInput input);
        Task<PlaqueToHerdCreateOrUpdateInput> CheckValidation(PlaqueToHerdCreateOrUpdateInput input);
        Task DeletePlaqueToHerd(EntityDto input);
    }
}