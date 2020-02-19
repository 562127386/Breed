
import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { OfficerServiceProxy, OfficerListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditOfficerModalComponent } from './create-or-edit-officer-modal.component';

@Component({
    templateUrl: './officer.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./officer.component.less']
})
export class OfficerComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditOfficerModal', { static: true }) createOrEditOfficerModal: CreateOrEditOfficerModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    officers: OfficerListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _officerService: OfficerServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getOfficer(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._officerService.getOfficer(
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

    createOfficer(): void {
        this.createOrEditOfficerModal.show();
    }

    deleteOfficer(officer: OfficerListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheOfficer', officer.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._officerService.deleteOfficer(officer.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
