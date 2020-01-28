import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    templateUrl: './contractor.component.html',
    styleUrls: ['./contractor.component.less'],
    animations: [appModuleAnimation()],
    encapsulation: ViewEncapsulation.None
})

export class ContractorComponent extends AppComponentBase {
   
    constructor(
        injector: Injector) {
        super(injector);
    }
}