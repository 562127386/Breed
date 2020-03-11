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
    [Table("PlaqueChanges")]
    public class PlaqueChange : Entity, IHasCreationTime, IMayHaveTenant
    {
        [ForeignKey("PrePlaqueId")]
        public virtual PlaqueInfo PrePlaque { get; set; }
        public long? PrePlaqueId { get; set; }
        
        [ForeignKey("NewPlaqueId")]
        public virtual PlaqueInfo NewPlaque { get; set; }
        public long? NewPlaqueId { get; set; }

        [Required]
        public string ChangeReson { get; set; }
        
        public DateTime SetTime { get; set; }

        [ForeignKey("StateId")]
        public virtual PlaqueState State { get; set; }
        public int? StateId { get; set; }
        
        [ForeignKey("OfficerId")]
        public virtual Officer Officer { get; set; }
        public int? OfficerId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }

        public PlaqueChange()
        {
            SetTime = Clock.Now;
        }
    }
}