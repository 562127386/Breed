import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BodyColorInfoServiceProxy, BodyColorInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditBodyColorInfoModalComponent } from './create-or-edit-bodyColorInfo-modal.component';

@Component({
    templateUrl: './bodyColorInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./bodyColorInfo.component.less']
})
export class BodyColorInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditBodyColorInfoModal', { static: true }) createOrEditBodyColorInfoModal: CreateOrEditBodyColorInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    bodyColorInfos: BodyColorInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _bodyColorInfoService: BodyColorInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getBodyColorInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._bodyColorInfoService.getBodyColorInfo(
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

    createBodyColorInfo(): void {
        this.createOrEditBodyColorInfoModal.show();
    }

    deleteBodyColorInfo(bodyColorInfo: BodyColorInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheBodyColorInfo', bodyColorInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._bodyColorInfoService.deleteBodyColorInfo(bodyColorInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
