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
using Akh.Breed.Officers;

namespace Akh.Breed.Inseminating
{
    [Table("AkhInseminating")]
    public class Insemination : Entity, IHasCreationTime, IMayHaveTenant, ICreationAudited
    {
        [Required]
        public string NationalCode { get; set; }

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public Insemination()
        {
            CreationTime = Clock.Now;
        }

        public long? CreatorUserId { get; set; }
    }
}