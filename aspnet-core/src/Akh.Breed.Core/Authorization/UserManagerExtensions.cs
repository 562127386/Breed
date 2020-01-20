using System.Threading.Tasks;
using Abp.Authorization.Users;
using Akh.Breed.Authorization.Users;

namespace Akh.Breed.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}

