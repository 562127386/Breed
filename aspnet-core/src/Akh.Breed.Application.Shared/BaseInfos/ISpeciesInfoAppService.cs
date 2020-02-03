using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface ISexInfoAppService : IApplicationService
    {
        Task<PagedResultDto<SexInfoListDto>> GetSexInfo(GetSexInfoInput input);
        Task<SexInfoCreateOrUpdateInput> GetSexInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateSexInfo(SexInfoCreateOrUpdateInput input);
        Task DeleteSexInfo(EntityDto input);
    }
}