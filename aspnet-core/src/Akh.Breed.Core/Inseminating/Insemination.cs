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
using Akh.Breed.Livestocks;
using Akh.Breed.Officers;

namespace Akh.Breed.Inseminating
{
    [Table("AkhInseminating")]
    public class Insemination : Entity, IHasCreationTime, IMayHaveTenant, ICreationAudited
    {
        [Required]
        public string NationalCode { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        [ForeignKey("SpeciesInfoId")]
        public SpeciesInfo SpeciesInfo { get; set; }
        public int? SpeciesInfoId { get; set; }

        [ForeignKey("BreedInfoId")]
        public BreedInfo BreedInfo { get; set; }
        public int? BreedInfoId { get; set; }

        [ForeignKey("SexInfoId")]
        public SexInfo SexInfo { get; set; }
        public int? SexInfoId { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [ForeignKey("HerdId")]
        public Herd Herd { get; set; }
        public int? HerdId { get; set; }

        [ForeignKey("ActivityInfoId")]
        public ActivityInfo ActivityInfo { get; set; }
        public int? ActivityInfoId { get; set; }

        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public int? OfficerId { get; set; }

        [ForeignKey("LivestockFatherId")]
        public Livestock LivestockFather { get; set; }
        public int? LivestockFatherId { get; set; }
        public string NationalCodeFather { get; set; }
        [ForeignKey("BreedInfoFatherId")]
        public BreedInfo BreedInfoFather { get; set; }
        public int? BreedInfoFatherId { get; set; }

        [ForeignKey("LivestockMotherId")]
        public Livestock LivestockMother { get; set; }
        public int? LivestockMotherId { get; set; }
        public string NationalCodeMother { get; set; }
        [ForeignKey("BreedInfoMotherId")]
        public BreedInfo BreedInfoMother { get; set; }
        public int? BreedInfoMotherId { get; set; }
        
        public string EarNumber { get; set; }
        
        public string BodyNumber { get; set; }
        
        public string ForeignRegistrationNumber { get; set; }
        
        [ForeignKey("BirthTypeInfoId")]
        public BirthTypeInfo BirthTypeInfo { get; set; }
        public int? BirthTypeInfoId { get; set; }
        
        [ForeignKey("AnomalyInfoId")]
        public AnomalyInfo AnomalyInfo { get; set; }
        public int? AnomalyInfoId { get; set; }
        
        [ForeignKey("MembershipInfoId")]
        public MembershipInfo MembershipInfo { get; set; }
        public int? MembershipInfoId { get; set; }

        public DateTime? IdIssueDate { get; set; }
        
        public string BloodShare { get; set; }
        
        public string BreedShare { get; set; }
        
        [ForeignKey("BodyColorInfoId")]
        public BodyColorInfo BodyColorInfo { get; set; }
        public int? BodyColorInfoId { get; set; }
        
        [ForeignKey("SpotColorInfoId")]
        public BodyColorInfo SpotColorInfo { get; set; }
        public int? SpotColorInfoId { get; set; }
        
        [ForeignKey("SpotConnectorInfoId")]
        public SpotConnectorInfo SpotConnectorInfo { get; set; }
        public int? SpotConnectorInfoId { get; set; }

        public string BreedName { get; set; }
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Insemination()
        {
            CreationTime = Clock.Now;
        }

        public long? CreatorUserId { get; set; }
    }
}