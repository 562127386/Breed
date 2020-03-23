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

namespace Akh.Breed.Herds
{
    [Table("AkhHerds")]
    public class Herd : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string HerdName { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public string AgriculturalId { get; set; }

        public bool ActivityStatus { get; set; }

        public bool LicenseStatus { get; set; }

        public string Institution { get; set; }
        
        public string LicenseNum { get; set; }

        public DateTime? IssueDate { get; set; }
        
        public DateTime? ValidityDate { get; set; }

        public bool Iranian { get; set; }

        public bool Reality { get; set; }

        public string Code { get; set; }
        
        [Required]
        public string NationalCode { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Family { get; set; }

        public string Mobile { get; set; }
        
        public string Phone { get; set; }

        public string Address { get; set; }
        
        [ForeignKey("EpidemiologicInfoId")]
        public EpidemiologicInfo EpidemiologicInfo { get; set; }
        public int? EpidemiologicInfoId { get; set; }

        [ForeignKey("UnionInfoId")]
        public UnionInfo UnionInfo { get; set; }
        public int? UnionInfoId { get; set; }

        [ForeignKey("VillageInfoId")]
        public VillageInfo VillageInfo { get; set; }
        public int? VillageInfoId { get; set; }

        [ForeignKey("ActivityInfoId")]
        public ActivityInfo ActivityInfo { get; set; }
        public int? ActivityInfoId { get; set; }

        [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }
        public int? ContractorId { get; set; }

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Herd()
        {
            CreationTime = Clock.Now;
        }
    }
}