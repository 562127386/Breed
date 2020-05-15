using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Plaques.Dto;

namespace Akh.Breed.Plaques
{
    public interface IPlaqueToContractorAppService : IApplicationService
    {
        Task<PagedResultDto<PlaqueToContractorListDto>> GetPlaqueToContractor(GetPlaqueToContractorInput input);
        Task<PlaqueToContractorGetForEditOutput> GetPlaqueToContractorForEdit(NullableIdDto<int> input);
        Task CreateOrUpdatePlaqueToContractor(PlaqueToContractorCreateOrUpdateInput input);
        Task DeletePlaqueToContractor(EntityDto input);
    }
}