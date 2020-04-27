import { Component, Injector, ViewChild, ViewEncapsulation, OnInit, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { LivestockServiceProxy, LivestockListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditLivestockModalComponent } from './create-or-edit-livestock-modal.component';

@Component({
    templateUrl: './livestock.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./livestock.component.less']
})
export class LivestockComponent extends AppComponentBase implements OnInit, AfterViewInit {

    @ViewChild('createOrEditLivestockModal', { static: true }) createOrEditLivestockModal: CreateOrEditLivestockModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    livestocks: LivestockListDto[] = [];
    filterText : string = '';
    isCreateForm : number = 0;

    constructor(
        injector: Injector,
        private _livestockService: LivestockServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
 
    ngOnInit() {
        // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
        this._activatedRoute.queryParams.subscribe(params => {
            this.isCreateForm = params['isCreateForm'];
            if(this.isCreateForm !== undefined && this.isCreateForm == 1){                
                this.createOrEditLivestockModal.show();
            }
          });
      }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getLivestock(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._livestockService.getLivestock(
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

    createLivestock(): void {
        this.createOrEditLivestockModal.show();
    }

    deleteLivestock(livestock: LivestockListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheLivestock', livestock.nationalCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._livestockService.deleteLivestock(livestock.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
