import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { BirthTypeInfoServiceProxy, BirthTypeInfoListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditBirthTypeInfoModalComponent } from './create-or-edit-birthTypeInfo-modal.component';

@Component({
    templateUrl: './birthTypeInfo.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./birthTypeInfo.component.less']
})
export class BirthTypeInfoComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditBirthTypeInfoModal', { static: true }) createOrEditBirthTypeInfoModal: CreateOrEditBirthTypeInfoModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    birthTypeInfos: BirthTypeInfoListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _birthTypeInfoService: BirthTypeInfoServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getBirthTypeInfo(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._birthTypeInfoService.getBirthTypeInfo(
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

    createBirthTypeInfo(): void {
        this.createOrEditBirthTypeInfoModal.show();
    }

    deleteBirthTypeInfo(birthTypeInfo: BirthTypeInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheBirthTypeInfo', birthTypeInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._birthTypeInfoService.deleteBirthTypeInfo(birthTypeInfo.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
