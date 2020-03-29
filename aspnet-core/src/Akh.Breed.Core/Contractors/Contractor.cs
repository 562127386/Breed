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

namespace Akh.Breed.Contractors
{
    [Table("AkhContractors")]
    public class Contractor : Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Institution { get; set; }
        
        public string SubInstitution { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string Code { get; set; }

        public string NationalCode { get; set; }

        public DateTime BirthDate { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        
        public string FirmName { get; set; }

        public string FirmRegNumber { get; set; }

        public string FirmEstablishmentYear { get; set; }

        public int FullTimeStaffDiploma { get; set; }
        
        public int FullTimeStaffAssociateDegree { get; set; }
        
        public int FullTimeStaffBachelorAndUpper { get; set; }
        
        public int PartialTimeStaffDiploma { get; set; }
        
        public int PartialTimeStaffAssociateDegree { get; set; }
        
        public int PartialTimeStaffBachelorAndUpper { get; set; }

        [ForeignKey("FirmTypeId")]
        public FirmType FirmType { get; set; }
        public int? FirmTypeId { get; set; }
        
        [ForeignKey("StateInfoId")]
        public StateInfo StateInfo { get; set; }
        public int? StateInfoId { get; set; }
        
        [ForeignKey("CityInfoId")]
        public CityInfo CityInfo { get; set; }
        public int? CityInfoId { get; set; }
        
        [ForeignKey("RegionInfoId")]
        public RegionInfo RegionInfo { get; set; }
        public int? RegionInfoId { get; set; }
        
        [ForeignKey("VillageInfoId")]
        public VillageInfo VillageInfo { get; set; }
        public int? VillageInfoId { get; set; }
        
        [ForeignKey("UnionInfoId")]
        public UnionInfo UnionInfo { get; set; }
        public int? UnionInfoId { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Contractor()
        {
            CreationTime = Clock.Now;
        }
    }
}