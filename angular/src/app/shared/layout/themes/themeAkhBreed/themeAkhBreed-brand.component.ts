import { Injector, Component, ViewEncapsulation, Inject } from '@angular/core';

import { AppConsts } from '@shared/AppConsts';
import { AppComponentBase } from '@shared/common/app-component-base';

import { DOCUMENT } from '@angular/common';

@Component({
    templateUrl: './themeAkhBreed-brand.component.html',
    selector: 'themeAkhBreed-brand',
    encapsulation: ViewEncapsulation.None
})
export class ThemeAkhBreedBrandComponent extends AppComponentBase {

    defaultLogo = AppConsts.appBaseUrl + '/assets/common/images/arm1.png';
    defaultLogo2 = AppConsts.appBaseUrl + '/assets/common/images/arm2.png';
    defaultLogo3 = AppConsts.appBaseUrl + '/assets/common/images/arm3.png';
    remoteServiceBaseUrl: string = AppConsts.remoteServiceBaseUrl;

    constructor(
        injector: Injector,
        @Inject(DOCUMENT) private document: Document
    ) {
        super(injector);
    }

    clickTopbarToggle(): void {
        this.document.body.classList.toggle('m-topbar--on');
    }
}
