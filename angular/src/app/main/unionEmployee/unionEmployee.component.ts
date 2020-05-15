import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { UnionEmployeeServiceProxy, UnionEmployeeListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditUnionEmployeeModalComponent } from './create-or-edit-unionEmployee-modal.component';

@Component({
    templateUrl: './unionEmployee.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./unionEmployee.component.less']
})
export class UnionEmployeeComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditUnionEmployeeModal', { static: true }) createOrEditUnionEmployeeModal: CreateOrEditUnionEmployeeModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    unionEmployees: UnionEmployeeListDto[] = [];
    filterText : string = '';
    unionInfoId?: number;

    constructor(
        injector: Injector,
        private _unionEmployeeService: UnionEmployeeServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _router: Router
    ) {
        super(injector);
    }
    
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
        this.primengTableHelper.adjustScroll(this.dataTable);
    }
    
    init(): void {
        this._activatedRoute.params.subscribe((params: Params) => {
            this.unionInfoId = params['id'];
            this.filterText = params['filterText'] || '';

            this.reloadPage();
        });
    }

    applyFilters(): void {
        this._router.navigate(['app/main/unionInfo', this.unionInfoId, 'employees', {
            filterText: this.filterText
        }]);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);

            return;
        }
    }

    getUnionEmployee(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable || !this.unionInfoId) {
            return;
        }
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._unionEmployeeService.getUnionEmployee(
            this.filterText,
            this.unionInfoId,
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

    createUnionEmployee(): void {
        this.createOrEditUnionEmployeeModal.show(this.unionInfoId);
    }

    deleteUnionEmployee(unionEmployee: UnionEmployeeListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheUnionEmployee', unionEmployee.name + ' ' + unionEmployee.family),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._unionEmployeeService.deleteUnionEmployee(unionEmployee.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
