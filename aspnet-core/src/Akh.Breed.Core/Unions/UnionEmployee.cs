using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Akh.Breed.Authorization.Users;
using Akh.Breed.BaseInfo;

namespace Akh.Breed.Unions
{
    [Table("AkhUnionEmployees")]
    public class UnionEmployee : Entity, IHasCreationTime, IMayHaveTenant
    {

        public string NationalCode { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string Phone { get; set; }

        public string Post { get; set; }

        [ForeignKey("UnionInfoId")]
        public virtual UnionInfo UnionInfo { get; set; }
        
        public int? UnionInfoId { get; set; }
        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public long? UserId { get; set; }

        public UnionEmployee()
        {
            CreationTime = Clock.Now;
        }
    }
}