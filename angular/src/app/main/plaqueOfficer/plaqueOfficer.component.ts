import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueOfficerServiceProxy, PlaqueOfficerListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueOfficerModalComponent } from './create-or-edit-plaqueOfficer-modal.component';

@Component({
    templateUrl: './plaqueOfficer.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueOfficer.component.less']
})
export class PlaqueOfficerComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditPlaqueOfficerModal', { static: true }) createOrEditPlaqueOfficerModal: CreateOrEditPlaqueOfficerModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueOfficers: PlaqueOfficerListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _plaqueOfficerService: PlaqueOfficerServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueOfficer(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueOfficerService.getPlaqueOfficer(
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

    createPlaqueOfficer(): void {
        this.createOrEditPlaqueOfficerModal.show();
    }

    deletePlaqueOfficer(plaqueOfficer: PlaqueOfficerListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueOfficer', plaqueOfficer.fromCode +'-'+ plaqueOfficer.toCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueOfficerService.deletePlaqueOfficer(plaqueOfficer.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
