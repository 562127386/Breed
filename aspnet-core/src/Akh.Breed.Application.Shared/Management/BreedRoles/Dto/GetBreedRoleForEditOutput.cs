﻿using System.Collections.Generic;
using Akh.Breed.Authorization.Permissions.Dto;

namespace Akh.Breed.Management.BreedRoles.Dto
{
    public class GetBreedRoleForEditOutput
    {
        public BreedRoleEditDto BreedRole { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
