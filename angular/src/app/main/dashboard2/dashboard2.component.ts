import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';

import { ActivatedRoute } from '@angular/router';

@Component({
    templateUrl: './dashboard2.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dashboard2.component.less']
})
export class Dashboard2Component extends AppComponentBase implements AfterViewInit {

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
    }
    
    ngAfterViewInit(): void {
    }  
}
