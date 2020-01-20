using System.Collections.Generic;
using Akh.Breed.Authorization.Permissions.Dto;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}
