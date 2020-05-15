import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueToContractorServiceProxy, PlaqueToContractorListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueToContractorModalComponent } from './create-or-edit-plaqueToContractor-modal.component';

@Component({
    templateUrl: './plaqueToContractor.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueToContractor.component.less']
})
export class PlaqueToContractorComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditPlaqueToContractorModal', { static: true }) createOrEditPlaqueToContractorModal: CreateOrEditPlaqueToContractorModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueToContractors: PlaqueToContractorListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _plaqueToContractorService: PlaqueToContractorServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueToContractor(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueToContractorService.getPlaqueToContractor(
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

    createPlaqueToContractor(): void {
        this.createOrEditPlaqueToContractorModal.show();
    }

    deletePlaqueToContractor(plaqueToContractor: PlaqueToContractorListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueToContractor', plaqueToContractor.fromCode +'-'+ plaqueToContractor.toCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueToContractorService.deletePlaqueToContractor(plaqueToContractor.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
