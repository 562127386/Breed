using System;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Officers.Dto
{
    public class OfficerCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        public string Code { get; set; }

        public string NationalCode { get; set; }
        
        public DateTime? BirthDate { get; set; }

        public string Name { get; set; }
        
        public string Family { get; set; }
        
        public string UserName { get; set; }
        
        public string FatherName { get; set; }
        
        public string IdNo { get; set; }

        public string WorkNumber { get; set; }

        public string MobileNumber { get; set; }
        
        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string AcademicDegreeName { get; set; }

        public int AcademicDegreeId { get; set; }

        public string StateInfoName { get; set; }

        public int StateInfoId { get; set; }

        public string ContractorName { get; set; }

        public int ContractorId { get; set; }

        public long UserId { get; set; }
    }
}