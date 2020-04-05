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
using Akh.Breed.Officers;

namespace Akh.Breed.Herds
{
    [Table("AkhHerdGeoLogs")]
    public class HerdGeoLog : Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
        
        [ForeignKey("HerdId")]
        public Herd Herd { get; set; }
        public int? HerdId { get; set; }
        
        [ForeignKey("OfficerId")]
        public Officer Officer { get; set; }
        public int? OfficerId { get; set; }

        public DateTime CreationTime { get; set; }
        
        public int? TenantId { get; set; }
        
        public HerdGeoLog()
        {
            CreationTime = Clock.Now;
        }
    }
}