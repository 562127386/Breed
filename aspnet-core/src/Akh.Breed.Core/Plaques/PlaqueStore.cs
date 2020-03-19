using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueStores")]
    public class PlaqueStore : Entity, IHasCreationTime, IMayHaveTenant
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
        
        public DateTime LastDate { get; set; }

        public DateTime SetTime { get; set; }

        [ForeignKey("SpeciesId")]
        public SpeciesInfo Species { get; set; }
        public int? SpeciesId { get; set; }

        [ForeignKey("FinishedPlaqueId")]
        public PlaqueInfo FinishedPlaque { get; set; }
        public long? FinishedPlaqueId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }

        public virtual ICollection<PlaqueOfficer> PlaqueOfficers { get; set; }

        public PlaqueStore()
        {
            CreationTime = Clock.Now;
            LastDate = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}