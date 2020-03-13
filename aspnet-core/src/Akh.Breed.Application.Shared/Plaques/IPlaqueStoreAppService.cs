using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueStoreAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueStoreListDto>> GetPlaqueStore(GetPlaqueStoreInput input);
        Task<PlaqueStoreGetForEditOutput> GetPlaqueStoreForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueStore(PlaqueStoreCreateOrUpdateInput input);
        Task DeletePlaqueStore(EntityDto input);
    }
}