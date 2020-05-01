using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueToCities")]
    public class PlaqueToCity : Entity, IHasCreationTime, IMayHaveTenant
    {
        public const int CodeLength = 15; 
        
        [Required]
        [StringLength(CodeLength)]
        public long FromCode { get; set; }
        
        [Required]
        [StringLength(CodeLength)]
        public long ToCode { get; set; }

        [StringLength(CodeLength)]
        public long LastCode { get; set; }
        
        public DateTime SetTime { get; set; }
        
        [ForeignKey("CityInfoId")] 
        public virtual CityInfo CityInfo { get; set; }
        public int? CityInfoId { get; set; }        
        
        [ForeignKey("PlaqueToStateId")]
        public virtual PlaqueToState PlaqueToState { get; set; }
        public int? PlaqueToStateId { get; set; }     

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public long PlaqueAllocated => PlaqueToOfficers.Sum(x => Convert.ToInt64(x.ToCode) - Convert.ToInt64(x.FromCode) + 1);
        
        public virtual ICollection<PlaqueToOfficer> PlaqueToOfficers { get; set; }
        
        public PlaqueToCity()
        {
            CreationTime = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}