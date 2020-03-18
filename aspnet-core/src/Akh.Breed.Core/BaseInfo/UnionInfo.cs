using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhUnionInfo")]
    public class UnionInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }

        public UnionInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}