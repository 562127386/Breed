import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HerdGeoLogServiceProxy, HerdGeoLogListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditHerdGeoLogModalComponent } from './create-or-edit-herdGeoLog-modal.component';

@Component({
    templateUrl: './herdGeoLog.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./herdGeoLog.component.less']
})
export class HerdGeoLogComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditHerdGeoLogModal', { static: true }) createOrEditHerdGeoLogModal: CreateOrEditHerdGeoLogModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    herdGeoLogs: HerdGeoLogListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _herdGeoLogService: HerdGeoLogServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getHerdGeoLog(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._herdGeoLogService.getHerdGeoLog(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createHerdGeoLog(): void {
        this.createOrEditHerdGeoLogModal.show();
    }

    deleteHerdGeoLog(herdGeoLog: HerdGeoLogListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheHerdGeoLog', herdGeoLog.herdName),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._herdGeoLogService.deleteHerdGeoLog(herdGeoLog.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
