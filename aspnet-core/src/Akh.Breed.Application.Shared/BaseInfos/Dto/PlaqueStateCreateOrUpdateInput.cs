﻿using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.BaseInfos.Dto
{
    public class PlaqueStateCreateOrUpdateInput
    {
        public int? Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
    }
}