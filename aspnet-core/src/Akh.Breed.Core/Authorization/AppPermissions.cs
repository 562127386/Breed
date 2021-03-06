﻿namespace Akh.Breed.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Support = "Pages.Administration.Support";
        public const string Pages_Administration_Support_Create = "Pages.Administration.Support.Create";
        public const string Pages_Administration_Support_Edit = "Pages.Administration.Support.Edit";
        public const string Pages_Administration_Support_Delete = "Pages.Administration.Support.Delete";

        public const string Pages_Administration_Notice = "Pages.Administration.Notice";
        public const string Pages_Administration_Notice_Create = "Pages.Administration.Notice.Create";
        public const string Pages_Administration_Notice_Edit = "Pages.Administration.Notice.Edit";
        public const string Pages_Administration_Notice_Delete = "Pages.Administration.Notice.Delete";
        
        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";
        
        //Breed-app BaseInfo
        
        public const string Pages_Monitoring = "Pages.Monitoring";

        public const string Pages_BaseInfo = "Pages.BaseInfo";
        
        public const string Pages_BaseInfo_StateInfo = "Pages.BaseInfo.StateInfo";
        public const string Pages_BaseInfo_StateInfo_Create = "Pages.BaseInfo.StateInfo.Create";
        public const string Pages_BaseInfo_StateInfo_Edit = "Pages.BaseInfo.StateInfo.Edit";
        public const string Pages_BaseInfo_StateInfo_Delete = "Pages.BaseInfo.StateInfo.Delete";
        
        public const string Pages_BaseInfo_CityInfo = "Pages.BaseInfo.CityInfo";
        public const string Pages_BaseInfo_CityInfo_Create = "Pages.BaseInfo.CityInfo.Create";
        public const string Pages_BaseInfo_CityInfo_Edit = "Pages.BaseInfo.CityInfo.Edit";
        public const string Pages_BaseInfo_CityInfo_Delete = "Pages.BaseInfo.CityInfo.Delete";
        
        public const string Pages_BaseInfo_RegionInfo = "Pages.BaseInfo.RegionInfo";
        public const string Pages_BaseInfo_RegionInfo_Create = "Pages.BaseInfo.RegionInfo.Create";
        public const string Pages_BaseInfo_RegionInfo_Edit = "Pages.BaseInfo.RegionInfo.Edit";
        public const string Pages_BaseInfo_RegionInfo_Delete = "Pages.BaseInfo.RegionInfo.Delete";
        
        public const string Pages_BaseInfo_VillageInfo = "Pages.BaseInfo.VillageInfo";
        public const string Pages_BaseInfo_VillageInfo_Create = "Pages.BaseInfo.VillageInfo.Create";
        public const string Pages_BaseInfo_VillageInfo_Edit = "Pages.BaseInfo.VillageInfo.Edit";
        public const string Pages_BaseInfo_VillageInfo_Delete = "Pages.BaseInfo.VillageInfo.Delete";
        
        public const string Pages_BaseInfo_ProviderInfo = "Pages.BaseInfo.ProviderInfo";
        public const string Pages_BaseInfo_ProviderInfo_Create = "Pages.BaseInfo.ProviderInfo.Create";
        public const string Pages_BaseInfo_ProviderInfo_Edit = "Pages.BaseInfo.ProviderInfo.Edit";
        public const string Pages_BaseInfo_ProviderInfo_Delete = "Pages.BaseInfo.ProviderInfo.Delete";
        
        public const string Pages_BaseInfo_ActivityInfo = "Pages.BaseInfo.ActivityInfo";
        public const string Pages_BaseInfo_ActivityInfo_Create = "Pages.BaseInfo.ActivityInfo.Create";
        public const string Pages_BaseInfo_ActivityInfo_Edit = "Pages.BaseInfo.ActivityInfo.Edit";
        public const string Pages_BaseInfo_ActivityInfo_Delete = "Pages.BaseInfo.ActivityInfo.Delete";
        
        public const string Pages_BaseInfo_AcademicDegree = "Pages.BaseInfo.AcademicDegree";
        public const string Pages_BaseInfo_AcademicDegree_Create = "Pages.BaseInfo.AcademicDegree.Create";
        public const string Pages_BaseInfo_AcademicDegree_Edit = "Pages.BaseInfo.AcademicDegree.Edit";
        public const string Pages_BaseInfo_AcademicDegree_Delete = "Pages.BaseInfo.AcademicDegree.Delete";
        
        public const string Pages_BaseInfo_Manufacturer = "Pages.BaseInfo.Manufacturer";
        public const string Pages_BaseInfo_Manufacturer_Create = "Pages.BaseInfo.Manufacturer.Create";
        public const string Pages_BaseInfo_Manufacturer_Edit = "Pages.BaseInfo.Manufacturer.Edit";
        public const string Pages_BaseInfo_Manufacturer_Delete = "Pages.BaseInfo.Manufacturer.Delete";
        
        public const string Pages_BaseInfo_SexInfo = "Pages.BaseInfo.SexInfo";
        public const string Pages_BaseInfo_SexInfo_Create = "Pages.BaseInfo.SexInfo.Create";
        public const string Pages_BaseInfo_SexInfo_Edit = "Pages.BaseInfo.SexInfo.Edit";
        public const string Pages_BaseInfo_SexInfo_Delete = "Pages.BaseInfo.SexInfo.Delete";
        
        public const string Pages_BaseInfo_SpeciesInfo = "Pages.BaseInfo.SpeciesInfo";
        public const string Pages_BaseInfo_SpeciesInfo_Create = "Pages.BaseInfo.SpeciesInfo.Create";
        public const string Pages_BaseInfo_SpeciesInfo_Edit = "Pages.BaseInfo.SpeciesInfo.Edit";
        public const string Pages_BaseInfo_SpeciesInfo_Delete = "Pages.BaseInfo.SpeciesInfo.Delete";
        
        public const string Pages_BaseInfo_FirmType = "Pages.BaseInfo.FirmType";
        public const string Pages_BaseInfo_FirmType_Create = "Pages.BaseInfo.FirmType.Create";
        public const string Pages_BaseInfo_FirmType_Edit = "Pages.BaseInfo.FirmType.Edit";
        public const string Pages_BaseInfo_FirmType_Delete = "Pages.BaseInfo.FirmType.Delete";
        
        public const string Pages_BaseInfo_PlaqueState = "Pages.BaseInfo.PlaqueState";
        public const string Pages_BaseInfo_PlaqueState_Create = "Pages.BaseInfo.PlaqueState.Create";
        public const string Pages_BaseInfo_PlaqueState_Edit = "Pages.BaseInfo.PlaqueState.Edit";
        public const string Pages_BaseInfo_PlaqueState_Delete = "Pages.BaseInfo.PlaqueState.Delete";
        
        //Breed-app BaseIntro
        
        public const string Pages_BaseIntro = "Pages.BaseIntro";
        
        public const string Pages_BaseIntro_UnionInfo = "Pages.BaseIntro.UnionInfo";
        public const string Pages_BaseIntro_UnionInfo_Create = "Pages.BaseIntro.UnionInfo.Create";
        public const string Pages_BaseIntro_UnionInfo_Edit = "Pages.BaseIntro.UnionInfo.Edit";
        public const string Pages_BaseIntro_UnionInfo_Delete = "Pages.BaseIntro.UnionInfo.Delete";
        
        public const string Pages_BaseIntro_UnionEmployee = "Pages.BaseIntro.UnionEmployee";
        public const string Pages_BaseIntro_UnionEmployee_Create = "Pages.BaseIntro.UnionEmployee.Create";
        public const string Pages_BaseIntro_UnionEmployee_Edit = "Pages.BaseIntro.UnionEmployee.Edit";
        public const string Pages_BaseIntro_UnionEmployee_Delete = "Pages.BaseIntro.UnionEmployee.Delete";
        
        public const string Pages_BaseIntro_Contractor = "Pages.BaseIntro.Contractor";
        public const string Pages_BaseIntro_Contractor_Create = "Pages.BaseIntro.Contractor.Create";
        public const string Pages_BaseIntro_Contractor_Edit = "Pages.BaseIntro.Contractor.Edit";
        public const string Pages_BaseIntro_Contractor_Delete = "Pages.BaseIntro.Contractor.Delete";
        
        public const string Pages_BaseIntro_Officer = "Pages.BaseIntro.Officer";
        public const string Pages_BaseIntro_Officer_Create = "Pages.BaseIntro.Officer.Create";
        public const string Pages_BaseIntro_Officer_Edit = "Pages.BaseIntro.Officer.Edit";
        public const string Pages_BaseIntro_Officer_Delete = "Pages.BaseIntro.Officer.Delete";
        
        public const string Pages_BaseIntro_Herd = "Pages.BaseIntro.Herd";
        public const string Pages_BaseIntro_Herd_Create = "Pages.BaseIntro.Herd.Create";
        public const string Pages_BaseIntro_Herd_Edit = "Pages.BaseIntro.Herd.Edit";
        public const string Pages_BaseIntro_Herd_Delete = "Pages.BaseIntro.Herd.Delete";        
        
        //Breed-app IdentityInfo
        
        public const string Pages_IdentityInfo = "Pages.IdentityInfo";
        
        public const string Pages_IdentityInfo_PlaqueStore = "Pages.IdentityInfo.PlaqueStore";
        public const string Pages_IdentityInfo_PlaqueStore_Create = "Pages.IdentityInfo.PlaqueStore.Create";
        public const string Pages_IdentityInfo_PlaqueStore_Edit = "Pages.IdentityInfo.PlaqueStore.Edit";
        public const string Pages_IdentityInfo_PlaqueStore_Delete = "Pages.IdentityInfo.PlaqueStore.Delete";
        
        public const string Pages_IdentityInfo_PlaqueToState = "Pages.IdentityInfo.PlaqueToState";
        public const string Pages_IdentityInfo_PlaqueToState_Create = "Pages.IdentityInfo.PlaqueToState.Create";
        public const string Pages_IdentityInfo_PlaqueToState_Edit = "Pages.IdentityInfo.PlaqueToState.Edit";
        public const string Pages_IdentityInfo_PlaqueToState_Delete = "Pages.IdentityInfo.PlaqueToState.Delete";        
                
        public const string Pages_IdentityInfo_PlaqueToContractor = "Pages.IdentityInfo.PlaqueToContractor";
        public const string Pages_IdentityInfo_PlaqueToContractor_Create = "Pages.IdentityInfo.PlaqueToContractor.Create";
        public const string Pages_IdentityInfo_PlaqueToContractor_Edit = "Pages.IdentityInfo.PlaqueToContractor.Edit";
        public const string Pages_IdentityInfo_PlaqueToContractor_Delete = "Pages.IdentityInfo.PlaqueToContractor.Delete";
                
        public const string Pages_IdentityInfo_PlaqueToOfficer = "Pages.IdentityInfo.PlaqueToOfficer";
        public const string Pages_IdentityInfo_PlaqueToOfficer_Create = "Pages.IdentityInfo.PlaqueToOfficer.Create";
        public const string Pages_IdentityInfo_PlaqueToOfficer_Edit = "Pages.IdentityInfo.PlaqueToOfficer.Edit";
        public const string Pages_IdentityInfo_PlaqueToOfficer_Delete = "Pages.IdentityInfo.PlaqueToOfficer.Delete";
        
        public const string Pages_IdentityInfo_PlaqueToHerd = "Pages.IdentityInfo.PlaqueToHerd";
        public const string Pages_IdentityInfo_PlaqueToHerd_Create = "Pages.IdentityInfo.PlaqueToHerd.Create";
        public const string Pages_IdentityInfo_PlaqueToHerd_Edit = "Pages.IdentityInfo.PlaqueToHerd.Edit";
        public const string Pages_IdentityInfo_PlaqueToHerd_Delete = "Pages.IdentityInfo.PlaqueToHerd.Delete";
        
        public const string Pages_IdentityInfo_Identification = "Pages.IdentityInfo.Identification";
        public const string Pages_IdentityInfo_Identification_Create = "Pages.IdentityInfo.Identification.Create";
        public const string Pages_IdentityInfo_Identification_Edit = "Pages.IdentityInfo.Identification.Edit";
        public const string Pages_IdentityInfo_Identification_Delete = "Pages.IdentityInfo.Identification.Delete";
        
        //Breed-app Activities
        
        public const string Pages_Activities = "Pages.Activities";
        
        public const string Pages_Activities_EditGeoHerd = "Pages.Activities.EditGeoHerd";
        public const string Pages_Activities_EditGeoHerd_Create = "Pages.Activities.EditGeoHerd.Create";
        public const string Pages_Activities_EditGeoHerd_Edit = "Pages.Activities.EditGeoHerd.Edit";
        public const string Pages_Activities_EditGeoHerd_Delete = "Pages.Activities.EditGeoHerd.Delete";
        
        public const string Pages_Activities_EditStatePlaque = "Pages.Activities.EditStatePlaque";
        public const string Pages_Activities_EditStatePlaque_Create = "Pages.Activities.EditStatePlaque.Create";
        public const string Pages_Activities_EditStatePlaque_Edit = "Pages.Activities.EditStatePlaque.Edit";
        public const string Pages_Activities_EditStatePlaque_Delete = "Pages.Activities.EditStatePlaque.Delete";
        
        public const string Pages_Activities_HerdCertificate = "Pages.Activities.HerdCertificate";
        public const string Pages_Activities_HerdCertificate_Create = "Pages.Activities.HerdCertificate.Create";
        public const string Pages_Activities_HerdCertificate_Edit = "Pages.Activities.HerdCertificate.Edit";
        public const string Pages_Activities_HerdCertificate_Delete = "Pages.Activities.HerdCertificate.Delete";
        
        //Breed-app Reports
        
        public const string Pages_Reports = "Pages.Reports";
        
        public const string Pages_Reports_HerdLivestock = "Pages.Reports.HerdLivestock";
        public const string Pages_Reports_HerdLivestock_Create = "Pages.Reports.HerdLivestock.Create";
        public const string Pages_Reports_HerdLivestock_Edit = "Pages.Reports.HerdLivestock.Edit";
        public const string Pages_Reports_HerdLivestock_Delete = "Pages.Reports.HerdLivestock.Delete";
        
        //Breed-app Inseminating
        
        public const string Pages_Inseminating = "Pages.Inseminating";
        
        public const string Pages_Inseminating_Insemination = "Pages.Inseminating.Insemination";
        public const string Pages_Inseminating_Insemination_Create = "Pages.Inseminating.Insemination.Create";
        public const string Pages_Inseminating_Insemination_Edit = "Pages.Inseminating.Insemination.Edit";
        public const string Pages_Inseminating_Insemination_Delete = "Pages.Inseminating.Insemination.Delete";
        
        public const string Pages_Inseminating_BreedInfo = "Pages.Inseminating.BreedInfo";
        public const string Pages_Inseminating_BreedInfo_Create = "Pages.Inseminating.BreedInfo.Create";
        public const string Pages_Inseminating_BreedInfo_Edit = "Pages.Inseminating.BreedInfo.Edit";
        public const string Pages_Inseminating_BreedInfo_Delete = "Pages.Inseminating.BreedInfo.Delete";
        
        public const string Pages_Inseminating_BirthTypeInfo = "Pages.Inseminating.BirthTypeInfo";
        public const string Pages_Inseminating_BirthTypeInfo_Create = "Pages.Inseminating.BirthTypeInfo.Create";
        public const string Pages_Inseminating_BirthTypeInfo_Edit = "Pages.Inseminating.BirthTypeInfo.Edit";
        public const string Pages_Inseminating_BirthTypeInfo_Delete = "Pages.Inseminating.BirthTypeInfo.Delete";
        
        public const string Pages_Inseminating_AnomalyInfo = "Pages.Inseminating.AnomalyInfo";
        public const string Pages_Inseminating_AnomalyInfo_Create = "Pages.Inseminating.AnomalyInfo.Create";
        public const string Pages_Inseminating_AnomalyInfo_Edit = "Pages.Inseminating.AnomalyInfo.Edit";
        public const string Pages_Inseminating_AnomalyInfo_Delete = "Pages.Inseminating.AnomalyInfo.Delete";
        
        public const string Pages_Inseminating_MembershipInfo = "Pages.Inseminating.MembershipInfo";
        public const string Pages_Inseminating_MembershipInfo_Create = "Pages.Inseminating.MembershipInfo.Create";
        public const string Pages_Inseminating_MembershipInfo_Edit = "Pages.Inseminating.MembershipInfo.Edit";
        public const string Pages_Inseminating_MembershipInfo_Delete = "Pages.Inseminating.MembershipInfo.Delete";
        
        public const string Pages_Inseminating_BodyColorInfo = "Pages.Inseminating.BodyColorInfo";
        public const string Pages_Inseminating_BodyColorInfo_Create = "Pages.Inseminating.BodyColorInfo.Create";
        public const string Pages_Inseminating_BodyColorInfo_Edit = "Pages.Inseminating.BodyColorInfo.Edit";
        public const string Pages_Inseminating_BodyColorInfo_Delete = "Pages.Inseminating.BodyColorInfo.Delete";
        
        public const string Pages_Inseminating_SpotConnectorInfo = "Pages.Inseminating.SpotConnectorInfo";
        public const string Pages_Inseminating_SpotConnectorInfo_Create = "Pages.Inseminating.SpotConnectorInfo.Create";
        public const string Pages_Inseminating_SpotConnectorInfo_Edit = "Pages.Inseminating.SpotConnectorInfo.Edit";
        public const string Pages_Inseminating_SpotConnectorInfo_Delete = "Pages.Inseminating.SpotConnectorInfo.Delete";


    }
}
