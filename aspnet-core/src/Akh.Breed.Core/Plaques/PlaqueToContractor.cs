using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueToContractors")]
    public class PlaqueToContractor : Entity, IHasCreationTime, IMayHaveTenant
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
        
        [ForeignKey("ContractorId")] 
        public virtual Contractor Contractor { get; set; }
        public int? ContractorId { get; set; }        
        
        [ForeignKey("PlaqueToStateId")]
        public virtual PlaqueToState PlaqueToState { get; set; }
        public int? PlaqueToStateId { get; set; }     

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public long PlaqueAllocated => PlaqueToOfficers.Sum(x => Convert.ToInt64(x.ToCode) - Convert.ToInt64(x.FromCode) + 1);
        
        public virtual ICollection<PlaqueToOfficer> PlaqueToOfficers { get; set; }
        
        public PlaqueToContractor()
        {
            CreationTime = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}