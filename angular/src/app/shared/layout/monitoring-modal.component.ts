import { Component, ViewChild, Injector, ViewEncapsulation, OnInit, AfterViewInit  } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { LivestockServiceProxy, MonitoringListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';
import { format } from 'path';
import { interval, Subscription } from 'rxjs';

@Component({
    selector: 'monitoringModal',
    templateUrl: './monitoring-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class MonitoringModalComponent extends AppComponentBase implements AfterViewInit  {

    @ViewChild('monitoringModal', {static: true}) modal: ModalDirective;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    
    monitorings: MonitoringListDto[] = [];
    filterText : string = '';

    active: boolean = false;

    constructor(
        injector: Injector,
        private _livestockService: LivestockServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = moment().format('YYYY-MM-DD HH:mm:ss');  
        interval(5000).subscribe(x => {
            if(this.active){
                this.getMonitorings();
            }
        });
    }

    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    show(): void {          
        this.filterText = moment().format('YYYY-MM-DD HH:mm:ss');
        this.active = true;
        this.modal.show();        
    }

    onShown(): void {        
    ;}

    getMonitorings(event?: LazyLoadEvent) {
        // if (this.primengTableHelper.shouldResetPaging(event)) {
        //     this.paginator.changePage(0);

        //     return;
        // }

        //this.primengTableHelper.showLoadingIndicator();
        

        this._livestockService.getMonitoring(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            //this.primengTableHelper.hideLoadingIndicator();
        });

            

    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}