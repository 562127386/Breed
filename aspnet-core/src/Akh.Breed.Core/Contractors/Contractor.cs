using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.Contractors
{
    [Table("AkhContractors")]
    public class Contractor : Entity<long>, IHasCreationTime, IMayHaveTenant
    {
        public string Institution { get; set; }
        
        public string SubInstitution { get; set; }

        public string Address { get; set; }

        public string NationalCode { get; set; }

        public DateTime BirthDate { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public ContractorFirmType FirmType { get; set; }

        public string FirmName { get; set; }

        public string FirmRegNumber { get; set; }

        public string FirmEstablishmentYear { get; set; }

        public int FullTimeStaffDiploma { get; set; }
        
        public int FullTimeStaffAssociateDegree { get; set; }
        
        public int FullTimeStaffBachelorAndUpper { get; set; }
        
        public int PartialTimeStaffDiploma { get; set; }
        
        public int PartialTimeStaffAssociateDegree { get; set; }
        
        public int PartialTimeStaffBachelorAndUpper { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Contractor(ContractorFirmType firmType)
        {
            if (!Enum.IsDefined(typeof(ContractorFirmType), firmType))
            {
                throw new Exception("Invalid FirmType value: " + firmType);
            }

            FirmType = firmType;
            CreationTime = Clock.Now;
        }
        
        protected Contractor()
        {
            
        }
    }
}