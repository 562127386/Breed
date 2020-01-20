using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Akh.Breed.Authorization.Permissions.Dto;
using Akh.Breed.Web.Areas.AppAreaName.Models.Common;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Users
{
    public class UsersViewModel : IPermissionsEditViewModel
    {
        public string FilterText { get; set; }

        public List<ComboboxItemDto> Roles { get; set; }

        public bool OnlyLockedUsers { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}
