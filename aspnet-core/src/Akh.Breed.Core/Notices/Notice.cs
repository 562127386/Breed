using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Authorization.Users;

namespace Akh.Breed.Notices
{
    [Table("AkhNotices")]
    public class Notice: Entity, IHasCreationTime, IMayHaveTenant
    {
        public const int MaxMessageLength = 4 * 1024; //4KB
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        [StringLength(MaxMessageLength)]
        public string Message { get; set; }

        public bool IsEnabled { get; set; }

        public NoticeType NoticeType { get; set; }
        
        public DateTime CreationTime { get; set; }
        public int? TenantId { get; set; }
        public virtual User User { get; set; }
        public long? UserId { get; set; }
        
        public Notice()
        {
            IsEnabled = true;
            CreationTime = Clock.Now;
        }
    }
}