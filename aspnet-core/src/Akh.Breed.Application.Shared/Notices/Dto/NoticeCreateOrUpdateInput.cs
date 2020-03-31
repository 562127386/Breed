namespace Akh.Breed.Notices.Dto
{
    public class NoticeCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Title { get; set; }
        
        public string Message { get; set; }

        public bool IsEnabled { get; set; }

        public NoticeType NoticeType { get; set; }

        public NoticeCreateOrUpdateInput()
        {
            IsEnabled = true;
        }
    }
}