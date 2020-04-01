using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Akh.Breed.Notices.Dto;

namespace Akh.Breed.Notices
{
    public interface INoticeAppService : IApplicationService
    {
        Task<PagedResultDto<NoticeListDto>> GetNotice(GetNoticeInput input);
        Task<PagedResultDto<NoticeListDto>> GetNews();
        Task<PagedResultDto<NoticeListDto>> GetInfos();
        Task<NoticeCreateOrUpdateInput> GetNoticeForEdit(NullableIdDto<int> input);
        Task CreateOrUpdateNotice(NoticeCreateOrUpdateInput input);
        Task DeleteNotice(EntityDto input);
        Task ToggleNotice(int? input);
    }
}