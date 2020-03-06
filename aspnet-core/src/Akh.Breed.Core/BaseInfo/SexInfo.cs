﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace Akh.Breed.BaseInfo
{

    [Table("AkhSexInfo")]
    public class SexInfo : Entity, IHasCreationTime, IMayHaveTenant
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreationTime { get; set; }

        public int? TenantId { get; set; }

        public SexInfo()
        {
            CreationTime = Clock.Now;
        }
    }
}