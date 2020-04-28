using System.Linq;
using Abp;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Notifications;
using Microsoft.EntityFrameworkCore;
using Akh.Breed.Authorization;
using Akh.Breed.Authorization.Roles;
using Akh.Breed.Authorization.Users;
using Akh.Breed.EntityFrameworkCore;
using Akh.Breed.Notifications;

namespace Akh.Breed.Migrations.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly BreedDbContext _context;

        public HostRoleAndUserCreator(BreedDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            var SysAdminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.SysAdmin);
            if (SysAdminRoleForHost == null)
            {
                SysAdminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.SysAdmin, StaticRoleNames.Host.SysAdmin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            
            var StateAdminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.StateAdmin);
            if (StateAdminRoleForHost == null)
            {
                StateAdminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.StateAdmin, StaticRoleNames.Host.StateAdmin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }
            
            var CityAdminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.CityAdmin);
            if (CityAdminRoleForHost == null)
            {
                CityAdminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.CityAdmin, StaticRoleNames.Host.CityAdmin) { IsStatic = true }).Entity;
                _context.SaveChanges();
            }

            var officerRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Officer);
            if (officerRoleForHost == null)
            {
                officerRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Officer, StaticRoleNames.Host.Officer) { IsStatic = true, IsDefault = true }).Entity;
                _context.SaveChanges();
            }

            //admin user for host

            var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
            if (adminUserForHost == null)
            {
                var user = new User
                {
                    TenantId = null,
                    UserName = AbpUserBase.AdminUserName,
                    Name = "admin",
                    Surname = "admin",
                    EmailAddress = "admin@aspnetzero.com",
                    IsEmailConfirmed = true,
                    ShouldChangePasswordOnNextLogin = false,
                    IsActive = true,
                    Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                };

                user.SetNormalizedNames();

                adminUserForHost = _context.Users.Add(user).Entity;
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                //User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = AbpUserBase.AdminUserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();

                //Notification subscriptions
                _context.NotificationSubscriptions.Add(new NotificationSubscriptionInfo(SequentialGuidGenerator.Instance.Create(), null, adminUserForHost.Id, AppNotificationNames.NewTenantRegistered));
                _context.NotificationSubscriptions.Add(new NotificationSubscriptionInfo(SequentialGuidGenerator.Instance.Create(), null, adminUserForHost.Id, AppNotificationNames.NewUserRegistered));

                _context.SaveChanges();
            }
        }
    }
}
