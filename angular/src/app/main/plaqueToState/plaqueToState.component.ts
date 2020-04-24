import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueToStateServiceProxy, PlaqueToStateListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueToStateModalComponent } from './create-or-edit-plaqueToState-modal.component';

@Component({
    templateUrl: './plaqueToState.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueToState.component.less']
})
export class PlaqueToStateComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditPlaqueToStateModal', { static: true }) createOrEditPlaqueToStateModal: CreateOrEditPlaqueToStateModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueToStates: PlaqueToStateListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _plaqueToStateService: PlaqueToStateServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueToState(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueToStateService.getPlaqueToState(
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

    createPlaqueToState(): void {
        this.createOrEditPlaqueToStateModal.show();
    }

    deletePlaqueToState(plaqueToState: PlaqueToStateListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueToState', plaqueToState.fromCode +'-'+ plaqueToState.toCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueToStateService.deletePlaqueToState(plaqueToState.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
