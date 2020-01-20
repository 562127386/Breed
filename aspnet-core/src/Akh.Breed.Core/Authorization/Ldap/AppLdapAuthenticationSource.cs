using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Akh.Breed.Authorization.Users;
using Akh.Breed.MultiTenancy;

namespace Akh.Breed.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
