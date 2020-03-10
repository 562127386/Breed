using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;
using Akh.Breed.Officers;

namespace Akh.Breed.Plaque
{
    [Table("AkhPlaqueInfos")]
    public class PlaqueInfo : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        public const int CodeLength = 15; 
        
        [Required]
        [StringLength(CodeLength)]
        public String Code { get; set; }

        public DateTime SetTime { get; set; }

        [Required]
        public string Longitude { get; set; }
        
        [Required]
        public string Latitude { get; set; }

        [ForeignKey("OfficerId")]
        public virtual Officer Officer { get; set; }
        public int OfficerId { get; set; }
        
        [ForeignKey("StateId")]
        public virtual PlaqueState State { get; set; }
        public int StateId { get; set; }
        
        public int? TenantId { get; set; }

        public DateTime CreationTime { get; set; }

        public PlaqueInfo(string code, string latitude, string longitude, Officer officer,PlaqueState state)
        {
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            OfficerId = officer.Id;
            StateId = state.Id;
            
            SetTime = Clock.Now;
        }
        
        public PlaqueInfo()
        {
            SetTime = Clock.Now;
        }
        
        public void ChangeState(PlaqueState newState)
        {
            State = newState;
        }
    }
}