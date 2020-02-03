using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IProviderInfoAppService : IApplicationService
    {
        ListResultDto<ProviderInfoListDto> GetProviderInfo(GetProviderInfoInput input);
        Task CreateProviderInfo(ProviderInfoCreateInput input);
        
        Task DeleteProviderInfo(EntityDto input);
    }
}