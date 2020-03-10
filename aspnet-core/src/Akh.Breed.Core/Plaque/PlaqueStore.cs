using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;

namespace Akh.Breed.Plaque
{
    [Table("AkhPlaqueStores")]
    public class PlaqueStore : Entity, IHasCreationTime, IMayHaveTenant
    {
        public const int CodeLength = 15; 
        
        [Required]
        [StringLength(CodeLength)]
        public String FromCode { get; set; }
        
        [Required]
        [StringLength(CodeLength)]
        public String ToCode { get; set; }
        
        public DateTime SetTime { get; set; }

        [ForeignKey("SpeciesId")]
        public virtual SpeciesInfo Species { get; set; }
        public int SpeciesId { get; set; }

        [ForeignKey("FinishedPlaqueId")]
        public virtual PlaqueInfo FinishedPlaque { get; set; }
        public long? FinishedPlaqueId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }

        public PlaqueStore()
        {
            SetTime = Clock.Now;
        }
    }
}