import { Component, Injector, ViewChild, ViewEncapsulation, OnInit, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { InseminationServiceProxy, InseminationListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditInseminationModalComponent } from './create-or-edit-insemination-modal.component';

@Component({
    templateUrl: './insemination.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./insemination.component.less']
})
export class InseminationComponent extends AppComponentBase implements OnInit, AfterViewInit {

    @ViewChild('createOrEditInseminationModal', { static: true }) createOrEditInseminationModal: CreateOrEditInseminationModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    inseminations: InseminationListDto[] = [];
    filterText : string = '';
    isCreateForm : number = 0;

    constructor(
        injector: Injector,
        private _inseminationService: InseminationServiceProxy,
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
                this.createOrEditInseminationModal.show();
            }
          });
      }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getInsemination(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._inseminationService.getInsemination(
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

    createInsemination(): void {
        this.createOrEditInseminationModal.show();
    }

    deleteInsemination(insemination: InseminationListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheInsemination', insemination.nationalCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._inseminationService.deleteInsemination(insemination.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
