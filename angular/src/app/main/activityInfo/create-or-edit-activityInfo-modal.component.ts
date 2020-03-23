import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ActivityInfoServiceProxy, ActivityInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditActivityInfoModal',
    templateUrl: './create-or-edit-activityInfo-modal.component.html'
})
export class CreateOrEditActivityInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    activityInfo: ActivityInfoCreateOrUpdateInput = new ActivityInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _activityInfoService: ActivityInfoServiceProxy
    ) {
        super(injector);
    }

    show(activityInfoId?: number,editdisabled?: boolean): void {        
        if (!activityInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._activityInfoService.getActivityInfoForEdit(activityInfoId).subscribe(userResult => {
            this.activityInfo.name = userResult.name;
            this.activityInfo.code = userResult.code;
            this.activityInfo.id =  activityInfoId;

            if (activityInfoId) {
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
        this._activityInfoService.createOrUpdateActivityInfo(this.activityInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.activityInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
