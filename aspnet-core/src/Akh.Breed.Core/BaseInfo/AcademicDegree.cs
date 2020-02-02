using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhAcademicDegree")]
    public class AcademicDegree : Entity, IHasCreationTime, IMayHaveTenant 
    {
        public string Code { get; set; }

        public string Name { get; set; }
        
        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public AcademicDegree()
        {
            CreationTime = Clock.Now;
        }
    }
}