import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SpotConnectorInfoServiceProxy, SpotConnectorInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSpotConnectorInfoModalComponent } from './create-or-edit-spotConnectorInfo-modal.component';

@Component({
    templateUrl: './spotConnectorInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./spotConnectorInfo.component.less']
})
export class SpotConnectorInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditSpotConnectorInfoModal', { static: true }) createOrEditSpotConnectorInfoModal: CreateOrEditSpotConnectorInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    spotConnectorInfos: SpotConnectorInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _spotConnectorInfoService: SpotConnectorInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSpotConnectorInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._spotConnectorInfoService.getSpotConnectorInfo(
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

    createSpotConnectorInfo(): void {
        this.createOrEditSpotConnectorInfoModal.show();
    }

    deleteSpotConnectorInfo(spotConnectorInfo: SpotConnectorInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheSpotConnectorInfo', spotConnectorInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._spotConnectorInfoService.deleteSpotConnectorInfo(spotConnectorInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
