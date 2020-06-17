using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IBirthTypeInfoAppService : IApplicationService
    {
        Task<PagedResultDto<BirthTypeInfoListDto>> GetBirthTypeInfo(GetBirthTypeInfoInput input);
        Task<BirthTypeInfoCreateOrUpdateInput> GetBirthTypeInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateBirthTypeInfo(BirthTypeInfoCreateOrUpdateInput input);
        Task DeleteBirthTypeInfo(EntityDto input);
    }
}