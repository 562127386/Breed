using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Contractors.Dto
{
    public class ContractorCreateDto
    {
        public long? Id { get; set; }
        
        public string Institution { get; set; }
        
        public string SubInstitution { get; set; }

        public string Address { get; set; }

        public string NationalCode { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ContractorFirmType? FirmType { get; set; }

        public string FirmName { get; set; }

        public string FirmRegNumber { get; set; }

        public string FirmEstablishmentYear { get; set; }

        public int? FullTimeStaffDiploma { get; set; }
        
        public int? FullTimeStaffAssociateDegree { get; set; }
        
        public int? FullTimeStaffBachelorAndUpper { get; set; }
        
        public int? PartialTimeStaffDiploma { get; set; }
        
        public int? PartialTimeStaffAssociateDegree { get; set; }
        
        public int? PartialTimeStaffBachelorAndUpper { get; set; }
    }
}