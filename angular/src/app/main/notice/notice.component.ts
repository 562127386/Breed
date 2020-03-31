import { Component, Injector, ViewChild, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NoticeServiceProxy, NoticeListDto } from '@shared/service-proxies/service-proxies';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { finalize } from 'rxjs/operators';
import { CreateOrEditNoticeModalComponent } from './create-or-edit-notice-modal.component';

@Component({
    templateUrl: './notice.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./notice.component.less']
})
export class NoticeComponent extends AppComponentBase implements AfterViewInit {

    @ViewChild('createOrEditNoticeModal', { static: true }) createOrEditNoticeModal: CreateOrEditNoticeModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    notices: NoticeListDto[] = [];
    filterText : string = '';

    constructor(
        injector: Injector,
        private _noticeService: NoticeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    
    ngAfterViewInit(): void {
        this.primengTableHelper.adjustScroll(this.dataTable);
    }

    getNotice(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._noticeService.getNotice(
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

    createNotice(): void {
        this.createOrEditNoticeModal.show();
    }

    deleteNotice(notice: NoticeListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheNotice', notice.title),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._noticeService.deleteNotice(notice.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 

    toggleNotice(notice: NoticeListDto): void {
        this.message.confirm(
            notice.isEnabled ? this.l('AreYouSureToDisableTheNotice', notice.title) :  this.l('AreYouSureToEnableTheNotice', notice.title),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._noticeService.toggleNotice(notice.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullySaved'));
                    });
                }
            }
        );
    } 
  
}
