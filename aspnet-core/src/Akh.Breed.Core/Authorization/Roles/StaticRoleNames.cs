namespace Akh.Breed.Authorization.Roles
{
    public static class StaticRoleNames
    {
        public static class Host
        {
            public const string Admin = "Admin";
            public const string SysAdmin = "SysAdmin";
            public const string StateAdmin = "StateAdmin";
            public const string CityAdmin = "CityAdmin";
            public const string Officer = "Officer";
        }

        public static class Tenants
        {
            public const string Admin = "Admin";

            public const string User = "User";
        }
    }
}
