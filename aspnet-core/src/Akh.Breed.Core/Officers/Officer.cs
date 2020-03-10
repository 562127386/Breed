using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;
using Akh.Breed.Contractors;

namespace Akh.Breed.Officers
{
    [Table("AkhOfficers")]
    public class Officer : Entity, IHasCreationTime, IMayHaveTenant
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

        [ForeignKey("AcademicDegreeId")]
        public AcademicDegree AcademicDegree { get; set; }

        public int AcademicDegreeId { get; set; }

        [ForeignKey("StateInfoId")]
        public StateInfo StateInfo { get; set; }

        public int StateInfoId { get; set; }

        [ForeignKey("ContractorId")]
        public Contractor Contractor { get; set; }

        public int ContractorId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public long UserId { get; set; }

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Officer()
        {
            CreationTime = Clock.Now;
        }
    }
}