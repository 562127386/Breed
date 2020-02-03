using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IProviderInfoAppService : IApplicationService
    {
        Task<PagedResultDto<ProviderInfoListDto>> GetProviderInfo(GetProviderInfoInput input);
        Task<ProviderInfoCreateOrUpdateInput> GetProviderInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateProviderInfo(ProviderInfoCreateOrUpdateInput input);
        Task DeleteProviderInfo(EntityDto input);
    }
}