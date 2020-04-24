using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueToCityAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueToCityListDto>> GetPlaqueToCity(GetPlaqueToCityInput input);
        Task<PlaqueToCityGetForEditOutput> GetPlaqueToCityForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueToCity(PlaqueToCityCreateOrUpdateInput input);
        Task DeletePlaqueToCity(EntityDto input);
    }
}