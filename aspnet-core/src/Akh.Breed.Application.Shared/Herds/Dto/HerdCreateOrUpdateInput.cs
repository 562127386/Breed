using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Herds.Dto
{
    public class HerdCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string HerdName { get; set; }
        
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }

        public string AgriculturalId { get; set; }

        public bool ActivityStatus { get; set; }

        public bool LicenseStatus { get; set; }

        public string LicenseNum { get; set; }

        public string Institution { get; set; }
        
        public DateTime? IssueDate { get; set; }
        
        public DateTime? ValidityDate { get; set; }

        public bool Iranian { get; set; }

        public bool Reality { get; set; }

        public string Code { get; set; }
        
        public string NationalCode { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Mobile { get; set; }
        
        public string Phone { get; set; }

        public string Address { get; set; }
        
        public int? EpidemiologicInfoId { get; set; }

        public int? UnionInfoId { get; set; }

        public int? ActivityInfoId { get; set; }

        public int? ContractorId { get; set; }       

        public int? StateInfoId { get; set; }
        public int? CityInfoId { get; set; }
        public int? RegionInfoId { get; set; }
        public int? VillageInfoId { get; set; }
    }
}