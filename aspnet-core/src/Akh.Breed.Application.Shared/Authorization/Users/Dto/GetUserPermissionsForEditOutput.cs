using System.Collections.Generic;
using Akh.Breed.Authorization.Permissions.Dto;

namespace Akh.Breed.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
