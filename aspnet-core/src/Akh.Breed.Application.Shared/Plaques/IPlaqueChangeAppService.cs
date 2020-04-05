using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueChangeAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueChangeListDto>> GetPlaqueChange(GetPlaqueChangeInput input);
        Task<PlaqueChangeGetForEditOutput> GetPlaqueChangeForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueChange(PlaqueChangeCreateOrUpdateInput input);
        Task<PlaqueChangeCreateOrUpdateInput> CheckValidation(PlaqueChangeCreateOrUpdateInput input);
        Task DeletePlaqueChange(EntityDto input);
        Task CheckPlaqueCode(string input);
    }
}