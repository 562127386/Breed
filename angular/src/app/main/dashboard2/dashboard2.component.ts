import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NoticeServiceProxy, NoticeListDto } from '@shared/service-proxies/service-proxies';

import { ActivatedRoute } from '@angular/router';

@Component({
    templateUrl: './dashboard2.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./dashboard2.component.less']
})
export class Dashboard2Component extends AppComponentBase implements AfterViewInit {

    news: NoticeListDto[] = [];
    infos: NoticeListDto[] = [];

    constructor(
        injector: Injector,
        private _noticeService: NoticeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
    }
    
    ngAfterViewInit(): void {
        this._noticeService.getNews()
        .subscribe(result => {
            this.news = result.items;
        });

        this._noticeService.getInfos()
        .subscribe(result => {
            this.infos = result.items;
        });
    }  
}
