using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Akh.Breed.Officers;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueOfficers")]
    public class PlaqueOfficer : Entity, IHasCreationTime, IMayHaveTenant
    {
        public const int CodeLength = 15; 
        
        [Required]
        [StringLength(CodeLength)]
        public String FromCode { get; set; }
        
        [Required]
        [StringLength(CodeLength)]
        public String ToCode { get; set; }
        
        public DateTime SetTime { get; set; }
        
        [ForeignKey("OfficerId")] 
        public virtual Officer Officer { get; set; }
        public int? OfficerId { get; set; }        
        
        [ForeignKey("FinishedPlaqueId")]
        public virtual PlaqueInfo FinishedPlaque { get; set; }
        public long? FinishedPlaqueId { get; set; }
        
        [ForeignKey("PlaqueStoreId")]
        public virtual PlaqueStore PlaqueStore { get; set; }
        public int? PlaqueStoreId { get; set; }     

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
    }
}