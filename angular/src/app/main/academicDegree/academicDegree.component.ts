import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AcademicDegreeServiceProxy, AcademicDegreeListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditAcademicDegreeModalComponent } from './create-or-edit-academicDegree-modal.component';

@Component({
    templateUrl: './academicDegree.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./academicDegree.component.less']
})
export class AcademicDegreeComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditAcademicDegreeModal', { static: true }) createOrEditAcademicDegreeModal: CreateOrEditAcademicDegreeModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    academicDegrees: AcademicDegreeListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _academicDegreeService: AcademicDegreeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getAcademicDegree(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._academicDegreeService.getAcademicDegree(
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

    createAcademicDegree(): void {
        this.createOrEditAcademicDegreeModal.show();
    }

    deleteAcademicDegree(academicDegree: AcademicDegreeListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheAcademicDegree', academicDegree.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._academicDegreeService.deleteAcademicDegree(academicDegree.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 
  
}
