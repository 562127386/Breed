﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Akh.Breed.Management.BreedRoles.Dto
{
    public class CreateOrUpdateBreedRoleInput
    {
        [Required]
        public BreedRoleEditDto BreedRole { get; set; }

        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}
