import { Component, Injector, ViewChild, ViewEncapsulation, OnInit, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PlaqueToHerdServiceProxy, PlaqueToHerdListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditPlaqueToHerdModalComponent } from './create-or-edit-plaqueToHerd-modal.component';

@Component({
    templateUrl: './plaqueToHerd.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./plaqueToHerd.component.less']
})
export class PlaqueToHerdComponent extends AppComponentBase implements OnInit, AfterViewInit {

    @ViewChild('createOrEditPlaqueToHerdModal', { static: true }) createOrEditPlaqueToHerdModal: CreateOrEditPlaqueToHerdModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    plaqueToHerds: PlaqueToHerdListDto[] = [];
    filterText : string = '';
    isCreateForm : number = 0;

    constructor(
        injector: Injector,
        private _plaqueToHerdService: PlaqueToHerdServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
 
    ngOnInit() {
        // Note: Below 'queryParams' can be replaced with 'params' depending on your requirements
        this._activatedRoute.queryParams.subscribe(params => {
            this.isCreateForm = params['isCreateForm'];
            if(this.isCreateForm !== undefined && this.isCreateForm == 1){                
                this.createOrEditPlaqueToHerdModal.show();
            }
          });
      }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getPlaqueToHerd(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._plaqueToHerdService.getPlaqueToHerd(
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

    createPlaqueToHerd(): void {
        this.createOrEditPlaqueToHerdModal.show();
    }

    deletePlaqueToHerd(plaqueToHerd: PlaqueToHerdListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteThePlaqueToHerd', plaqueToHerd.nationalCode),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._plaqueToHerdService.deletePlaqueToHerd(plaqueToHerd.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
