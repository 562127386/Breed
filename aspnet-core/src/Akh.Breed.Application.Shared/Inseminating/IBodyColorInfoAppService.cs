using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IBodyColorInfoAppService : IApplicationService
    {
        Task<PagedResultDto<BodyColorInfoListDto>> GetBodyColorInfo(GetBodyColorInfoInput input);
        Task<BodyColorInfoCreateOrUpdateInput> GetBodyColorInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateBodyColorInfo(BodyColorInfoCreateOrUpdateInput input);
        Task DeleteBodyColorInfo(EntityDto input);
    }
}