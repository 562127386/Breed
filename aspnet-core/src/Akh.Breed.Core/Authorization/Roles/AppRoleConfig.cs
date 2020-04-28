using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Akh.Breed.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host,
                    grantAllPermissionsByDefault: true)
            );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.SysAdmin,
                    MultiTenancySides.Host)
            );
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.StateAdmin,
                    MultiTenancySides.Host)
            );
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.CityAdmin,
                    MultiTenancySides.Host)
            );
            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Officer,
                    MultiTenancySides.Host)
            );

            //Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant,
                    grantAllPermissionsByDefault: true)
                );

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.User,
                    MultiTenancySides.Tenant)
                );
        }
    }
}

