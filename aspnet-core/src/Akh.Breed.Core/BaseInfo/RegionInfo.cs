﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{
    [Table("AkhVillageInfo")]
    public class VillageInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }
        
        public virtual CityInfo CityInfo { get; set; }

        public virtual int CityInfoId { get; set; }

        public VillageInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}