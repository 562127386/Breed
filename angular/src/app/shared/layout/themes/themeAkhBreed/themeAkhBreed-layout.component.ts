import { Injector, ElementRef, Component, OnInit } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ThemesLayoutBaseComponent } from '@app/shared/layout/themes/themes-layout-base.component';
import { UrlHelper } from '@shared/helpers/UrlHelper';
import { AppConsts } from '@shared/AppConsts';
import { OffcanvasOptions } from '@metronic/app/core/_base/layout/directives/offcanvas.directive';
import * as momentj from 'jalali-moment';

@Component({
    templateUrl: './themeAkhBreed-layout.component.html',
    selector: 'themeAkhBreed-layout',
    animations: [appModuleAnimation()]
})
export class ThemeAkhBreedLayoutComponent extends ThemesLayoutBaseComponent implements OnInit {

    menuCanvasOptions: OffcanvasOptions = {
        baseClass: 'kt-aside',
        overlay: true,
        closeBy: 'kt_aside_close_btn',
        toggleBy: {
            target: 'kt_aside_mobile_toggler',
            state: 'kt-header-mobile__toolbar-toggler--active'
        }
    };

    remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;
    time = momentj().format('jYYYY/jM/jD HH:mm:ss')
    installationMode;
    defaultLogo;
    defaultLogo2;
    defaultLogo3;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    ngOnInit() {
        this.installationMode = UrlHelper.isInstallUrl(location.href);
        this.defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/arm1.png';        
        this.defaultLogo2 = AppConsts.appBaseUrl + '/assets/common/images/arm2.png';        
        this.defaultLogo3 = AppConsts.appBaseUrl + '/assets/common/images/arm3.png';        

        setInterval(() => {
            this.time = momentj().format('jYYYY/jM/jD HH:mm:ss');
            }, 1000);
        
    }
}
