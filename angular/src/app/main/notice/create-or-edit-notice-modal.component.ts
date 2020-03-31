import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NoticeServiceProxy, NoticeCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditNoticeModal',
    templateUrl: './create-or-edit-notice-modal.component.html'
})
export class CreateOrEditNoticeModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    notice: NoticeCreateOrUpdateInput = new NoticeCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _noticeService: NoticeServiceProxy
    ) {
        super(injector);
    }

    show(noticeId?: number,editdisabled?: boolean): void {        
        if (!noticeId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._noticeService.getNoticeForEdit(noticeId).subscribe(userResult => {
            this.notice.title = userResult.title;
            this.notice.message = userResult.message;
            this.notice.isEnabled = userResult.isEnabled;
            this.notice.noticeType = userResult.noticeType;
            this.notice.id =  noticeId;

            if (noticeId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._noticeService.createOrUpdateNotice(this.notice)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.notice);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
