using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        public PlaqueToCity()
        {
            CreationTime = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}