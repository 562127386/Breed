﻿using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Management.BreedRoles.Dto
{
    public class BreedRoleEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string DisplayName { get; set; }
        
        public bool IsDefault { get; set; }
    }
}
