using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;
using Akh.Breed.Officers;

namespace Akh.Breed.Plaques
{
    [Table("AkhPlaqueChanges")]
    public class PlaqueChange : Entity, IHasCreationTime, IMayHaveTenant
    {
        [ForeignKey("PlaqueId")]
        public virtual PlaqueInfo Plaque { get; set; }
        public long? PlaqueId { get; set; }

        public string ChangeReason { get; set; }
        
        [ForeignKey("PreStateId")]
        public virtual PlaqueState PreState { get; set; }
        public int? PreStateId { get; set; }
        
        [ForeignKey("NewStateId")]
        public virtual PlaqueState NewState { get; set; }
        public int? NewStateId { get; set; }
        
        [ForeignKey("OfficerId")]
        public virtual Officer Officer { get; set; }
        public int? OfficerId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }

        public PlaqueChange()
        {
            CreationTime = Clock.Now;
        }
    }
}