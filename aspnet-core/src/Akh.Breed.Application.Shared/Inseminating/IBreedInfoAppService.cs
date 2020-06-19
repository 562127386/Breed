using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IBreedInfoAppService : IApplicationService
    {
        Task<PagedResultDto<BreedInfoListDto>> GetBreedInfo(GetBreedInfoInput input);
        Task<BreedInfoCreateOrUpdateInput> GetBreedInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateBreedInfo(BreedInfoCreateOrUpdateInput input);
        Task DeleteBreedInfo(EntityDto input);
    }
}