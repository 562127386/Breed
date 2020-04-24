import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueToCityServiceProxy, PlaqueToCityListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueToCityModalComponent } from './create-or-edit-plaqueToCity-modal.component';

@Component({
    templateUrl: './plaqueToCity.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueToCity.component.less']
})
export class PlaqueToCityComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditPlaqueToCityModal', { static: true }) createOrEditPlaqueToCityModal: CreateOrEditPlaqueToCityModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueToCitys: PlaqueToCityListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _plaqueToCityService: PlaqueToCityServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueToCity(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueToCityService.getPlaqueToCity(
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

    createPlaqueToCity(): void {
        this.createOrEditPlaqueToCityModal.show();
    }

    deletePlaqueToCity(plaqueToCity: PlaqueToCityListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueToCity', plaqueToCity.fromCode +'-'+ plaqueToCity.toCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueToCityService.deletePlaqueToCity(plaqueToCity.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
