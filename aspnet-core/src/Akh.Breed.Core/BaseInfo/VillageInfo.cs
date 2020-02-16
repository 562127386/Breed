using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhVillageInfo")]
    public class VillageInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
        
        [ForeignKey("RegionInfoId")]
        public virtual RegionInfo RegionInfo { get; set; }

        public virtual int RegionInfoId { get; set; }

        public VillageInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}