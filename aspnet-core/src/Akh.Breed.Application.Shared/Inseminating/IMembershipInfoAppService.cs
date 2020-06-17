using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Inseminating.Dto;

namespace Akh.Breed.Inseminating
{
    public interface IMembershipInfoAppService : IApplicationService
    {
        Task<PagedResultDto<MembershipInfoListDto>> GetMembershipInfo(GetMembershipInfoInput input);
        Task<MembershipInfoCreateOrUpdateInput> GetMembershipInfoForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateMembershipInfo(MembershipInfoCreateOrUpdateInput input);
        Task DeleteMembershipInfo(EntityDto input);
    }
}