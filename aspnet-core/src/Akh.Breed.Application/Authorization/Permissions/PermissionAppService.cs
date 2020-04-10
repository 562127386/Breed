using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Runtime.Session;
using Akh.Breed.Authorization.Permissions.Dto;
using Akh.Breed.Authorization.Roles;

namespace Akh.Breed.Authorization.Permissions
{
    public class PermissionAppService : BreedAppServiceBase, IPermissionAppService
    {
        public ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions().AsQueryable();
            var user = UserManager.GetUserById(AbpSession.GetUserId());
            var isAdmin = UserManager.IsInRoleAsync(user,StaticRoleNames.Host.Admin).Result;
            if (!isAdmin)
            {
                permissions = permissions.Where(x => (x.Name != AppPermissions.Pages_DemoUiComponents) && 
                                                     (x.Name != AppPermissions.Pages_Administration_Languages) &&
                                                     (x.Name != AppPermissions.Pages_Administration_AuditLogs) &&
                                                     (x.Name != AppPermissions.Pages_Administration_OrganizationUnits) &&
                                                     (x.Name != AppPermissions.Pages_Administration_UiCustomization) &&
                                                     (x.Name != AppPermissions.Pages_Tenant_Dashboard) &&
                                                     (x.Name != AppPermissions.Pages_Administration_Tenant_Settings) &&
                                                     (x.Name != AppPermissions.Pages_Administration_Tenant_SubscriptionManagement) &&
                                                     (x.Name != AppPermissions.Pages_Editions) &&
                                                     (x.Name != AppPermissions.Pages_Tenants) &&
                                                     (x.Name != AppPermissions.Pages_Administration_Host_Settings) &&
                                                     (x.Name != AppPermissions.Pages_Administration_Host_Maintenance) &&
                                                     (x.Name != AppPermissions.Pages_Administration_HangfireDashboard) &&
                                                     (x.Name != AppPermissions.Pages_Administration_Host_Dashboard) );
            }
            var rootPermissions = permissions.Where(p => p.Parent == null);

            var result = new List<FlatPermissionWithLevelDto>();

            foreach (var rootPermission in rootPermissions)
            {
                var level = 0;
                AddPermission(rootPermission, permissions.ToList(), result, level);
            }

            return new ListResultDto<FlatPermissionWithLevelDto>
            {
                Items = result
            };
        }

        private void AddPermission(Permission permission, IReadOnlyList<Permission> allPermissions, List<FlatPermissionWithLevelDto> result, int level)
        {
            var flatPermission = ObjectMapper.Map<FlatPermissionWithLevelDto>(permission);
            flatPermission.Level = level;
            result.Add(flatPermission);

            if (permission.Children == null)
            {
                return;
            }

            var children = allPermissions.Where(p => p.Parent != null && p.Parent.Name == permission.Name).ToList();

            foreach (var childPermission in children)
            {
                AddPermission(childPermission, allPermissions, result, level + 1);
            }
        }
    }
}
