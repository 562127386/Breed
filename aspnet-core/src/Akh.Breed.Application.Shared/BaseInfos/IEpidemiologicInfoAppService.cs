using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IEpidemiologicInfoAppService : IApplicationService
    {
        Task<PagedResultDto<EpidemiologicInfoListDto>> GetEpidemiologicInfo(GetEpidemiologicInfoInput input);
        Task<EpidemiologicInfoCreateOrUpdateInput> GetEpidemiologicInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateEpidemiologicInfo(EpidemiologicInfoCreateOrUpdateInput input);
        Task DeleteEpidemiologicInfo(EntityDto input);
    }
}