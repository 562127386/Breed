using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhRegionInfo")]
    public class RegionInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
        
        [ForeignKey("CityInfoId")]
        public virtual CityInfo CityInfo { get; set; }

        public virtual int CityInfoId { get; set; }

        public virtual ICollection<VillageInfo> Villages { get; set; }

        public RegionInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}