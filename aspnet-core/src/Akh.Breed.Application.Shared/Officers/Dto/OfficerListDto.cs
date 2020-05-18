using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Officers.Dto
{
    public class GetOfficerInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }

            Filter = Filter?.Trim();
        }
    }
    public class OfficerListDto : EntityDto
    {
        public string Code { get; set; }

        public string NationalCode { get; set; }
        
        public DateTime BirthDate { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }
        
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
    }
}