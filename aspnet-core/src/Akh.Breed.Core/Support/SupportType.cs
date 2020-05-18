using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.Support
{
    [Table("AkhSupportTypes")]
    public class SupportType : Entity, IHasCreationTime, IMayHaveTenant 
    {
        [Required]
        public string Name { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public SupportType()
        {
            CreationTime = Clock.Now;
        }
    }
}