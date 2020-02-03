import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SexInfoServiceProxy, SexInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSexInfoModalComponent } from "./CreateOrEditSexInfoModalComponent";

@Component({
    templateUrl: './sexInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./sexInfo.component.less']
})
export class SexInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditSexInfoModal', { static: true }) createOrEditSexInfoModal: CreateOrEditSexInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    sexInfos: SexInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _sexInfoService: SexInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSexInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._sexInfoService.getSexInfo(
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

    createSexInfo(): void {
        this.createOrEditSexInfoModal.show();
    }

    deleteSexInfo(sexInfo: SexInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheSexInfo', sexInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._sexInfoService.deleteSexInfo(sexInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
