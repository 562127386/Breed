using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Notices.Dto
{
    public class GetNoticeInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime,Title";
            }

            Filter = Filter?.Trim();
        }
    }
    
    public class NoticeListDto : EntityDto
    {
        public string Title { get; set; }
        
        public string Message { get; set; }

        public bool IsEnabled { get; set; }
        
        public string UserName { get; set; }

        public NoticeType NoticeType { get; set; }
    }
}