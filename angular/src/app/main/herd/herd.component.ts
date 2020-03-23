import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HerdServiceProxy, HerdListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditHerdModalComponent } from './create-or-edit-herd-modal.component';

@Component({
    templateUrl: './herd.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./herd.component.less']
})
export class HerdComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditHerdModal', { static: true }) createOrEditHerdModal: CreateOrEditHerdModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    herds: HerdListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _herdService: HerdServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getHerd(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._herdService.getHerd(
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

    createHerd(): void {
        this.createOrEditHerdModal.show();
    }

    deleteHerd(herd: HerdListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheHerd', herd.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._herdService.deleteHerd(herd.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
