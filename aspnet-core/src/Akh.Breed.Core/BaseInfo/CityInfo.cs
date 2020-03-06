using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhCityInfo")]
    public class CityInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }

        public virtual ICollection<VillageInfo> Villages { get; set; }
        
        [ForeignKey("StateInfoId")]
        public virtual StateInfo StateInfo { get; set; }
        
        public virtual int StateInfoId { get; set; }
        
        public CityInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}