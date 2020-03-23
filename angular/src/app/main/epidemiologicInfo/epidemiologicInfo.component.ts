import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { EpidemiologicInfoServiceProxy, EpidemiologicInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditEpidemiologicInfoModalComponent } from './create-or-edit-epidemiologicInfo-modal.component';

@Component({
    templateUrl: './epidemiologicInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./epidemiologicInfo.component.less']
})
export class EpidemiologicInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditEpidemiologicInfoModal', { static: true }) createOrEditEpidemiologicInfoModal: CreateOrEditEpidemiologicInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    epidemiologicInfos: EpidemiologicInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _epidemiologicInfoService: EpidemiologicInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getEpidemiologicInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._epidemiologicInfoService.getEpidemiologicInfo(
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

    createEpidemiologicInfo(): void {
        this.createOrEditEpidemiologicInfoModal.show();
    }

    deleteEpidemiologicInfo(epidemiologicInfo: EpidemiologicInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheEpidemiologicInfo', epidemiologicInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._epidemiologicInfoService.deleteEpidemiologicInfo(epidemiologicInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
