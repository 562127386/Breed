using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;
using Akh.Breed.Herds;
using Akh.Breed.Officers;

namespace Akh.Breed.Livestocks
{
    [Table("AkhLivestocks")]
    public class Livestock : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public bool Imported { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        
        [ForeignKey("SpeciesInfoId")]
        public SpeciesInfo SpeciesInfo { get; set; }
        public int? SpeciesInfoId { get; set; }

        [ForeignKey("SexInfoId")]
        public SexInfo SexInfo { get; set; }
        public int? SexInfoId { get; set; }

        [ForeignKey("HerdId")]
        public Herd Herd { get; set; }
        public int? HerdId { get; set; }

        [ForeignKey("ActivityInfoId")]
        public ActivityInfo ActivityInfo { get; set; }
        public int? ActivityInfoId { get; set; }

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public int? OfficerId { get; set; }

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Livestock()
        {
            CreationTime = Clock.Now;
        }
    }
}