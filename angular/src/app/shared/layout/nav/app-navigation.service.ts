import { PermissionCheckerService } from '@abp/auth/permission-checker.service';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {

    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) {

    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem('BaseInfo', 'Pages.BaseInfo', 'flaticon-folder-1', '',[
                new AppMenuItem('StateInfo', 'Pages.BaseInfo.StateInfo', 'flaticon-placeholder-1', '/app/main/stateInfo'),
                new AppMenuItem('CityInfo', 'Pages.BaseInfo.CityInfo', 'flaticon-placeholder', '/app/main/cityInfo'),
                new AppMenuItem('RegionInfo', 'Pages.BaseInfo.RegionInfo', 'flaticon-placeholder', '/app/main/regionInfo'),
                new AppMenuItem('VillageInfo', 'Pages.BaseInfo.VillageInfo', 'flaticon-placeholder-2', '/app/main/villageInfo'),
                new AppMenuItem('ProviderInfo', 'Pages.BaseInfo.ProviderInfo', 'flaticon-truck', '/app/main/providerInfo'),
                new AppMenuItem('ActivityInfo', 'Pages.BaseInfo.ActivityInfo', 'flaticon-truck', '/app/main/activityInfo'),
                new AppMenuItem('AcademicDegree', 'Pages.BaseInfo.AcademicDegree', 'flaticon-lock', '/app/main/academicDegree'),
                new AppMenuItem('SexInfo', 'Pages.BaseInfo.SexInfo', 'flaticon-technology-1', '/app/main/sexInfo'),
                new AppMenuItem('SpeciesInfo', 'Pages.BaseInfo.SpeciesInfo', 'flaticon-interface-6', '/app/main/speciesInfo'),
                new AppMenuItem('FirmType', 'Pages.BaseInfo.FirmType', 'flaticon-line-chart-1', '/app/main/firmType'),
                new AppMenuItem('PlaqueState', 'Pages.BaseInfo.PlaqueState', 'flaticon-technology-2', '/app/main/plaqueState'),
                new AppMenuItem('Manufacturer', 'Pages.BaseInfo.Manufacturer', 'flaticon-technology-2', '/app/main/manufacturer')
            ]),
            new AppMenuItem('BaseIntro', 'Pages.BaseIntro', 'flaticon-folder-1', '',[
                new AppMenuItem('UnionInfo', 'Pages.BaseIntro.UnionInfo', 'flaticon-truck', '/app/main/unionInfo'),
                new AppMenuItem('Contractor', 'Pages.BaseIntro.Contractor', 'flaticon-diagram', '/app/main/contractor'),
                new AppMenuItem('Officer', 'Pages.BaseIntro.Officer', 'flaticon-diagram', '/app/main/officer'),                
                new AppMenuItem('Herd', 'Pages.BaseIntro.Herd', 'flaticon2-box', '/app/main/herd')
            ]),
            new AppMenuItem('IdentityInfo', 'Pages.IdentityInfo', 'flaticon-folder-1', '',[                
                new AppMenuItem('PlaqueStore', 'Pages.IdentityInfo.PlaqueStore', 'flaticon2-box', '/app/main/plaqueStore'),
                new AppMenuItem('PlaqueToState', 'Pages.IdentityInfo.PlaqueToState', 'flaticon2-box', '/app/main/plaqueToState'),
                new AppMenuItem('PlaqueToContractor', 'Pages.IdentityInfo.PlaqueToContractor', 'flaticon2-box', '/app/main/plaqueToContractor'),
                new AppMenuItem('PlaqueToOfficer', 'Pages.IdentityInfo.PlaqueToOfficer', 'flaticon2-box', '/app/main/plaqueToOfficer')
            ]),            
            new AppMenuItem('Activities', 'Pages.Activities', 'flaticon2-box', '',[
                new AppMenuItem('Identification', 'Pages.IdentityInfo.Identification', 'flaticon2-box', '/app/main/livestock'),
                new AppMenuItem('PlaqueToHerd', 'Pages.IdentityInfo.PlaqueToHerd', 'flaticon2-box', '/app/main/plaqueToHerd'),
                new AppMenuItem('EditGeoHerd', 'Pages.Activities.EditGeoHerd', 'flaticon2-box', '/app/main/herdGeoLog'),
                new AppMenuItem('EditStatePlaque', 'Pages.Activities.EditStatePlaque', 'flaticon2-box', '/app/main/plaqueChange'),
                new AppMenuItem('HerdCertificate', 'Pages.Activities.HerdCertificate', 'flaticon2-box', null)
            ]),
            new AppMenuItem('Reports', 'Pages.Reports', 'flaticon2-box', ''),
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
                new AppMenuItem('Notice', 'Pages.Administration.Notice', 'flaticon-technology-2', '/app/main/notice'),
                new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages'),
                new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),
                new AppMenuItem('VisualSettings', 'Pages.Administration.UiCustomization', 'flaticon-medical', '/app/admin/ui-customization'),
                new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ]),
            new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components')

        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {

        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null || subMenuItem.permissionName && this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                return true;
            } else if (subMenuItem.items && subMenuItem.items.length) {
                return this.checkChildMenuItemPermission(subMenuItem);
            }
        }

        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' && this._appSessionService.tenant && !this._appSessionService.tenant.edition) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }

    /**
     * Returns all menu items recursively
     */
    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach(menuItem => {
            allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });

        return allMenuItems;
    }

    private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
        if (!menuItem.items) {
            return [menuItem];
        }

        let menuItems = [menuItem];
        menuItem.items.forEach(subMenu => {
            menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
        });

        return menuItems;
    }
}
