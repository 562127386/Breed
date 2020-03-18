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
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
            new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [
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
            new AppMenuItem('DemoUiComponents', 'Pages.DemoUiComponents', 'flaticon-shapes', '/app/admin/demo-ui-components'),

            new AppMenuItem('BaseInfo', null, 'flaticon-folder-1', '',[
                new AppMenuItem('StateInfo', null, 'flaticon-placeholder-1', '/app/main/stateInfo'),
                new AppMenuItem('CityInfo', null, 'flaticon-placeholder', '/app/main/cityInfo'),
                new AppMenuItem('RegionInfo', null, 'flaticon-placeholder', '/app/main/regionInfo'),
                new AppMenuItem('VillageInfo', null, 'flaticon-placeholder-2', '/app/main/villageInfo'),
                new AppMenuItem('ProviderInfo', null, 'flaticon-truck', '/app/main/providerInfo'),
                new AppMenuItem('UnionInfo', null, 'flaticon-truck', '/app/main/unionInfo'),
                new AppMenuItem('ActivityInfo', null, 'flaticon-truck', '/app/main/activityInfo'),
                new AppMenuItem('AcademicDegree', null, 'flaticon-lock', '/app/main/academicDegree'),
                new AppMenuItem('SexInfo', null, 'flaticon-technology-1', '/app/main/sexInfo'),
                new AppMenuItem('SpeciesInfo', null, 'flaticon-interface-6', '/app/main/speciesInfo'),
                new AppMenuItem('FirmType', null, 'flaticon-line-chart-1', '/app/main/firmType'),
                new AppMenuItem('PlaqueState', null, 'flaticon-technology-2', '/app/main/plaqueState')
            ]),
            new AppMenuItem('BaseIntro', null, 'flaticon-folder-1', '',[
                new AppMenuItem('Contractor', null, 'flaticon-diagram', '/app/main/contractor'),
                new AppMenuItem('Officer', null, 'flaticon-diagram', '/app/main/officer'),                
                new AppMenuItem('Herd', null, 'flaticon2-box', '')
            ]),
            new AppMenuItem('IdentityInfo', null, 'flaticon-folder-1', '',[                
                new AppMenuItem('PlaqueStore', null, 'flaticon2-box', '/app/main/plaqueStore'),
                new AppMenuItem('PlaqueOfficer', null, 'flaticon2-box', '/app/main/plaqueOfficer'),
                new AppMenuItem('Identification', null, 'flaticon2-box', '')
            ]),            
            new AppMenuItem('Activities', null, 'flaticon2-box', ''),
            new AppMenuItem('Reports', null, 'flaticon2-box', '')


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
