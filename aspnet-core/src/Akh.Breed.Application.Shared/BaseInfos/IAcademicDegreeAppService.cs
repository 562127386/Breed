using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface ISpeciesInfoAppService : IApplicationService
    {
        Task<PagedResultDto<SpeciesInfoListDto>> GetSpeciesInfo(GetSpeciesInfoInput input);
        Task<SpeciesInfoCreateOrUpdateInput> GetSpeciesInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateSpeciesInfo(SpeciesInfoCreateOrUpdateInput input);
        Task DeleteSpeciesInfo(EntityDto input);
    }
}