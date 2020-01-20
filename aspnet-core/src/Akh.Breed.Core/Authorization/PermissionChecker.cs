using Abp.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;

namespace Akh.Breed.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

