import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SpeciesInfoServiceProxy, SpeciesInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditSpeciesInfoModalComponent } from './create-or-edit-speciesInfo-modal.component';

@Component({
    templateUrl: './speciesInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./speciesInfo.component.less']
})
export class SpeciesInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditSpeciesInfoModal', { static: true }) createOrEditSpeciesInfoModal: CreateOrEditSpeciesInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    speciesInfos: SpeciesInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _speciesInfoService: SpeciesInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getSpeciesInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._speciesInfoService.getSpeciesInfo(
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

    createSpeciesInfo(): void {
        this.createOrEditSpeciesInfoModal.show();
    }

    deleteSpeciesInfo(speciesInfo: SpeciesInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheSpeciesInfo', speciesInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._speciesInfoService.deleteSpeciesInfo(speciesInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
