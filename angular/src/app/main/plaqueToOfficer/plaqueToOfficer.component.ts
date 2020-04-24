import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueToOfficerServiceProxy, PlaqueToOfficerListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueToOfficerModalComponent } from './create-or-edit-plaqueToOfficer-modal.component';

@Component({
    templateUrl: './plaqueToOfficer.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueToOfficer.component.less']
})
export class PlaqueToOfficerComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditPlaqueToOfficerModal', { static: true }) createOrEditPlaqueToOfficerModal: CreateOrEditPlaqueToOfficerModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueToOfficers: PlaqueToOfficerListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _plaqueToOfficerService: PlaqueToOfficerServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueToOfficer(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueToOfficerService.getPlaqueToOfficer(
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

    createPlaqueToOfficer(): void {
        this.createOrEditPlaqueToOfficerModal.show();
    }

    deletePlaqueToOfficer(plaqueToOfficer: PlaqueToOfficerListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueToOfficer', plaqueToOfficer.fromCode +'-'+ plaqueToOfficer.toCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueToOfficerService.deletePlaqueToOfficer(plaqueToOfficer.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
