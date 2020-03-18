import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ActivityInfoServiceProxy, ActivityInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditActivityInfoModalComponent } from './create-or-edit-activityInfo-modal.component';

@Component({
    templateUrl: './activityInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./activityInfo.component.less']
})
export class ActivityInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditActivityInfoModal', { static: true }) createOrEditActivityInfoModal: CreateOrEditActivityInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    activityInfos: ActivityInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _activityInfoService: ActivityInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getActivityInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._activityInfoService.getActivityInfo(
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

    createActivityInfo(): void {
        this.createOrEditActivityInfoModal.show();
    }

    deleteActivityInfo(activityInfo: ActivityInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheActivityInfo', activityInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._activityInfoService.deleteActivityInfo(activityInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
