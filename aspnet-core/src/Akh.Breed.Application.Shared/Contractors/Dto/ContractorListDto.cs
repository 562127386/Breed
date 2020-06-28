using System;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using Akh.Breed.Dto;

namespace Akh.Breed.Contractors.Dto
{
    public class GetContractorInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "CreationTime DESC";
            }
            Sorting = Sorting.Replace("stateInfoName", "StateInfo.Name");
            Filter = Filter?.Trim();
        }
    }
    public class ContractorListDto : EntityDto
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

        public int FirmTypeId { get; set; }
        
        public string FirmTypeName { get; set; }
        
        public string StateInfoName { get; set; }
        
        public string CreationTimeStr { get; set; }
    }
}