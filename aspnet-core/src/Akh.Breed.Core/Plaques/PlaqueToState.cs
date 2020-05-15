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
    [Table("AkhPlaqueToStates")]
    public class PlaqueToState : Entity, IHasCreationTime, IMayHaveTenant
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
        
        [ForeignKey("StateInfoId")] 
        public virtual StateInfo StateInfo { get; set; }
        public int? StateInfoId { get; set; }        
        
        [ForeignKey("PlaqueStoreId")]
        public virtual PlaqueStore PlaqueStore { get; set; }
        public int? PlaqueStoreId { get; set; }     

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public long PlaqueAllocated => PlaqueToContractors.Sum(x => Convert.ToInt64(x.ToCode) - Convert.ToInt64(x.FromCode) + 1);
        
        public virtual ICollection<PlaqueToContractor> PlaqueToContractors { get; set; }
        
        public PlaqueToState()
        {
            CreationTime = Clock.Now;
            SetTime = Clock.Now;
        }
    }
}