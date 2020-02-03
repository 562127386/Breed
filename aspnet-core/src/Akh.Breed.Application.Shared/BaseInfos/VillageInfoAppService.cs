using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.BaseInfos.Dto;

namespace Akh.Breed.BaseInfos
{
    public interface IStateInfoAppService : IApplicationService
    {
        Task<PagedResultDto<StateInfoListDto>> GetStateInfo(GetStateInfoInput input);
        Task<StateInfoCreateOrUpdateInput> GetStateInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateStateInfo(StateInfoCreateOrUpdateInput input);
        Task DeleteStateInfo(EntityDto input);
    }
}