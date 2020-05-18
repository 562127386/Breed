using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Support.Dto;

namespace Akh.Breed.Support
{
    public interface ISupportAppService : IApplicationService
    {
        Task<PagedResultDto<SupportListDto>> GetSupport(GetSupportInput input);
        Task<SupportGetForEditOutput> GetSupportForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateSupport(SupportCreateOrUpdateInput input);
        Task DeleteSupport(EntityDto input);
    }
}