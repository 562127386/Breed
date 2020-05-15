using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Officers;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueToOfficers")]
    public class PlaqueToOfficer : Entity, IHasCreationTime, IMayHaveTenant
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
        
        [ForeignKey("OfficerId")] 
        public virtual Officer Officer { get; set; }
        public int? OfficerId { get; set; }
        
        [ForeignKey("PlaqueToContractorId")]
        public virtual PlaqueToContractor PlaqueToContractor { get; set; }
        public int? PlaqueToContractorId { get; set; }     

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public PlaqueToOfficer()
        {
            CreationTime = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}