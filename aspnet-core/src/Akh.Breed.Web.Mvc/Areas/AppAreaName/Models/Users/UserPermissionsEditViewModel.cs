using Abp.AutoMapper;
using Akh.Breed.Authorization.Users;
using Akh.Breed.Authorization.Users.Dto;
using Akh.Breed.Web.Areas.AppAreaName.Models.Common;

namespace Akh.Breed.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}
