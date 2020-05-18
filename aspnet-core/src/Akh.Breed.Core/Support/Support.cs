using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Authorization.Users;

namespace Akh.Breed.Support
{
    [Table("AkhSupport")]
    public class Support : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string Description { get; set; }
        
        public string Response { get; set; }
        
        [ForeignKey("SupportTypeId")]
        public SupportType SupportType { get; set; }
        public int? SupportTypeId { get; set; }
        
        [ForeignKey("SupportStateId")]
        public SupportState SupportState { get; set; }
        public int? SupportStateId { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        public long? UserId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Support()
        {
            CreationTime = Clock.Now;
        }
    }
}