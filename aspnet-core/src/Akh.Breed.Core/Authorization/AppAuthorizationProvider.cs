using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Akh.Breed.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var notice = administration.CreateChildPermission(AppPermissions.Pages_Administration_Notice, L("Notice"));
            notice.CreateChildPermission(AppPermissions.Pages_Administration_Notice_Create, L("CreateNewNotice"));
            notice.CreateChildPermission(AppPermissions.Pages_Administration_Notice_Edit, L("EditNewNotice"));
            notice.CreateChildPermission(AppPermissions.Pages_Administration_Notice_Delete, L("DeleteNewNotice"));
            
            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host); 

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
            
            //Breed-app BaseInfo

            var baseInfo = pages.CreateChildPermission(AppPermissions.Pages_BaseInfo, L("BaseInfo"));

            var stateInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_StateInfo, L("StateInfo"));
            stateInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_StateInfo_Create, L("CreateNewStateInfo"));
            stateInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_StateInfo_Edit, L("EditNewStateInfo"));
            stateInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_StateInfo_Delete, L("DeleteNewStateInfo"));

            var cityInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_CityInfo, L("CityInfo"));
            cityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_CityInfo_Create, L("CreateNewCityInfo"));
            cityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_CityInfo_Edit, L("EditNewCityInfo"));
            cityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_CityInfo_Delete, L("DeleteNewCityInfo"));

            var regionInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_RegionInfo, L("RegionInfo"));
            regionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_RegionInfo_Create, L("CreateNewRegionInfo"));
            regionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_RegionInfo_Edit, L("EditNewRegionInfo"));
            regionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_RegionInfo_Delete, L("DeleteNewRegionInfo"));

            var villageInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_VillageInfo, L("VillageInfo"));
            villageInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_VillageInfo_Create, L("CreateNewVillageInfo"));
            villageInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_VillageInfo_Edit, L("EditNewVillageInfo"));
            villageInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_VillageInfo_Delete, L("DeleteNewVillageInfo"));

            var providerInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ProviderInfo, L("ProviderInfo"));
            providerInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ProviderInfo_Create, L("CreateNewProviderInfo"));
            providerInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ProviderInfo_Edit, L("EditNewProviderInfo"));
            providerInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ProviderInfo_Delete, L("DeleteNewProviderInfo"));

            var unionInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_UnionInfo, L("UnionInfo"));
            unionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_UnionInfo_Create, L("CreateNewUnionInfo"));
            unionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_UnionInfo_Edit, L("EditNewUnionInfo"));
            unionInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_UnionInfo_Delete, L("DeleteNewUnionInfo"));

            var activityInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ActivityInfo, L("ActivityInfo"));
            activityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ActivityInfo_Create, L("CreateNewActivityInfo"));
            activityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ActivityInfo_Edit, L("EditNewActivityInfo"));
            activityInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_ActivityInfo_Delete, L("DeleteNewActivityInfo"));

            var academicDegree = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_AcademicDegree, L("AcademicDegree"));
            academicDegree.CreateChildPermission(AppPermissions.Pages_BaseInfo_AcademicDegree_Create, L("CreateNewAcademicDegree"));
            academicDegree.CreateChildPermission(AppPermissions.Pages_BaseInfo_AcademicDegree_Edit, L("EditNewAcademicDegree"));
            academicDegree.CreateChildPermission(AppPermissions.Pages_BaseInfo_AcademicDegree_Delete, L("DeleteNewAcademicDegree"));

            var manufacturer = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_Manufacturer, L("Manufacturer"));
            manufacturer.CreateChildPermission(AppPermissions.Pages_BaseInfo_Manufacturer_Create, L("CreateNewManufacturer"));
            manufacturer.CreateChildPermission(AppPermissions.Pages_BaseInfo_Manufacturer_Edit, L("EditNewManufacturer"));
            manufacturer.CreateChildPermission(AppPermissions.Pages_BaseInfo_Manufacturer_Delete, L("DeleteNewManufacturer"));

            var sexInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SexInfo, L("SexInfo"));
            sexInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SexInfo_Create, L("CreateNewSexInfo"));
            sexInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SexInfo_Edit, L("EditNewSexInfo"));
            sexInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SexInfo_Delete, L("DeleteNewSexInfo"));

            var speciesInfo = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SpeciesInfo, L("SpeciesInfo"));
            speciesInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SpeciesInfo_Create, L("CreateNewSpeciesInfo"));
            speciesInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SpeciesInfo_Edit, L("EditNewSpeciesInfo"));
            speciesInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_SpeciesInfo_Delete, L("DeleteNewSpeciesInfo"));

            var firmType = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_FirmType, L("FirmType"));
            firmType.CreateChildPermission(AppPermissions.Pages_BaseInfo_FirmType_Create, L("CreateNewFirmType"));
            firmType.CreateChildPermission(AppPermissions.Pages_BaseInfo_FirmType_Edit, L("EditNewFirmType"));
            firmType.CreateChildPermission(AppPermissions.Pages_BaseInfo_FirmType_Delete, L("DeleteNewFirmType"));

            var plaqueState = baseInfo.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaqueState, L("PlaqueState"));
            plaqueState.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaqueState_Create, L("CreateNewPlaqueState"));
            plaqueState.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaqueState_Edit, L("EditNewPlaqueState"));
            plaqueState.CreateChildPermission(AppPermissions.Pages_BaseInfo_PlaqueState_Delete, L("DeleteNewPlaqueState"));
            
            //Breed-app BaseInfo

            var baseIntro = pages.CreateChildPermission(AppPermissions.Pages_BaseIntro, L("BaseIntro"));

            var contractor = baseIntro.CreateChildPermission(AppPermissions.Pages_BaseIntro_Contractor, L("Contractor"));
            contractor.CreateChildPermission(AppPermissions.Pages_BaseIntro_Contractor_Create, L("CreateNewContractor"));
            contractor.CreateChildPermission(AppPermissions.Pages_BaseIntro_Contractor_Edit, L("EditNewContractor"));
            contractor.CreateChildPermission(AppPermissions.Pages_BaseIntro_Contractor_Delete, L("DeleteNewContractor"));

            var officer = baseIntro.CreateChildPermission(AppPermissions.Pages_BaseIntro_Officer, L("Officer"));
            officer.CreateChildPermission(AppPermissions.Pages_BaseIntro_Officer_Create, L("CreateNewOfficer"));
            officer.CreateChildPermission(AppPermissions.Pages_BaseIntro_Officer_Edit, L("EditNewOfficer"));
            officer.CreateChildPermission(AppPermissions.Pages_BaseIntro_Officer_Delete, L("DeleteNewOfficer"));

            var herd = baseIntro.CreateChildPermission(AppPermissions.Pages_BaseIntro_Herd, L("Herd"));
            herd.CreateChildPermission(AppPermissions.Pages_BaseIntro_Herd_Create, L("CreateNewHerd"));
            herd.CreateChildPermission(AppPermissions.Pages_BaseIntro_Herd_Edit, L("EditNewHerd"));
            herd.CreateChildPermission(AppPermissions.Pages_BaseIntro_Herd_Delete, L("DeleteNewHerd"));
            
            //Breed-app BaseInfo

            var identityInfo = pages.CreateChildPermission(AppPermissions.Pages_IdentityInfo, L("IdentityInfo"));

            var plaqueStore = identityInfo.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueStore, L("PlaqueStore"));
            plaqueStore.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueStore_Create, L("CreateNewPlaqueStore"));
            plaqueStore.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueStore_Edit, L("EditNewPlaqueStore"));
            plaqueStore.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueStore_Delete, L("DeleteNewPlaqueStore"));

            var plaqueToState = identityInfo.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToState, L("PlaqueToState"));
            plaqueToState.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToState_Create, L("CreateNewPlaqueToState"));
            plaqueToState.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToState_Edit, L("EditNewPlaqueToState"));
            plaqueToState.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToState_Delete, L("DeleteNewPlaqueToState"));
            
            var plaqueToCity = identityInfo.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToCity, L("PlaqueToCity"));
            plaqueToCity.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Create, L("CreateNewPlaqueToCity"));
            plaqueToCity.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Edit, L("EditNewPlaqueToCity"));
            plaqueToCity.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToCity_Delete, L("DeleteNewPlaqueToCity"));
            
            var plaqueToOfficer = identityInfo.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer, L("PlaqueToOfficer"));
            plaqueToOfficer.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Create, L("CreateNewPlaqueToOfficer"));
            plaqueToOfficer.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Edit, L("EditNewPlaqueToOfficer"));
            plaqueToOfficer.CreateChildPermission(AppPermissions.Pages_IdentityInfo_PlaqueToOfficer_Delete, L("DeleteNewPlaqueToOfficer"));

            var identification = identityInfo.CreateChildPermission(AppPermissions.Pages_IdentityInfo_Identification, L("Identification"));
            identification.CreateChildPermission(AppPermissions.Pages_IdentityInfo_Identification_Create, L("CreateNewIdentification"));
            identification.CreateChildPermission(AppPermissions.Pages_IdentityInfo_Identification_Edit, L("EditNewIdentification"));
            identification.CreateChildPermission(AppPermissions.Pages_IdentityInfo_Identification_Delete, L("DeleteNewIdentification"));
            
            //Breed-app BaseInfo

            var activities = pages.CreateChildPermission(AppPermissions.Pages_Activities, L("Activities"));

            var editGeoHerd = activities.CreateChildPermission(AppPermissions.Pages_Activities_EditGeoHerd, L("EditGeoHerd"));
            editGeoHerd.CreateChildPermission(AppPermissions.Pages_Activities_EditGeoHerd_Create, L("CreateNewEditGeoHerd"));
            editGeoHerd.CreateChildPermission(AppPermissions.Pages_Activities_EditGeoHerd_Edit, L("EditNewEditGeoHerd"));
            editGeoHerd.CreateChildPermission(AppPermissions.Pages_Activities_EditGeoHerd_Delete, L("DeleteNewEditGeoHerd"));

            var editStatePlaque = activities.CreateChildPermission(AppPermissions.Pages_Activities_EditStatePlaque, L("EditStatePlaque"));
            editStatePlaque.CreateChildPermission(AppPermissions.Pages_Activities_EditStatePlaque_Create, L("CreateNewEditStatePlaque"));
            editStatePlaque.CreateChildPermission(AppPermissions.Pages_Activities_EditStatePlaque_Edit, L("EditNewEditStatePlaque"));
            editStatePlaque.CreateChildPermission(AppPermissions.Pages_Activities_EditStatePlaque_Delete, L("DeleteNewEditStatePlaque"));
            
            //Breed-app BaseInfo

            var reports = pages.CreateChildPermission(AppPermissions.Pages_Reports, L("Reports"));

            var editGeoHerd2 = reports.CreateChildPermission(AppPermissions.Pages_Reports_EditGeoHerd, L("EditGeoHerd"));
            editGeoHerd2.CreateChildPermission(AppPermissions.Pages_Reports_EditGeoHerd_Create, L("CreateNewEditGeoHerd"));
            editGeoHerd2.CreateChildPermission(AppPermissions.Pages_Reports_EditGeoHerd_Edit, L("EditNewEditGeoHerd"));
            editGeoHerd2.CreateChildPermission(AppPermissions.Pages_Reports_EditGeoHerd_Delete, L("DeleteNewEditGeoHerd"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BreedConsts.LocalizationSourceName);
        }
    }
}

