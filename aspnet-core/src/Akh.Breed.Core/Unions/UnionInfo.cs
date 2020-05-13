using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;

namespace Akh.Breed.Union
{
    [Table("AkhUnionInfo")]
    public class UnionInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string UnionName { get; set; }

        [ForeignKey("StateInfoId")]
        public virtual StateInfo StateInfo { get; set; }
        
        public int? StateInfoId { get; set; }
        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public long? UserId { get; set; }

        public UnionInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}