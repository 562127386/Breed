import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { EpidemiologicInfoServiceProxy, EpidemiologicInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditEpidemiologicInfoModal',
    templateUrl: './create-or-edit-epidemiologicInfo-modal.component.html'
})
export class CreateOrEditEpidemiologicInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    epidemiologicInfo: EpidemiologicInfoCreateOrUpdateInput = new EpidemiologicInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _epidemiologicInfoService: EpidemiologicInfoServiceProxy
    ) {
        super(injector);
    }

    show(epidemiologicInfoId?: number,editdisabled?: boolean): void {        
        if (!epidemiologicInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._epidemiologicInfoService.getEpidemiologicInfoForEdit(epidemiologicInfoId).subscribe(userResult => {
            this.epidemiologicInfo.name = userResult.name;
            this.epidemiologicInfo.family = userResult.family;
            this.epidemiologicInfo.code = userResult.code;
            this.epidemiologicInfo.id =  epidemiologicInfoId;

            if (epidemiologicInfoId) {
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
        this._epidemiologicInfoService.createOrUpdateEpidemiologicInfo(this.epidemiologicInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.epidemiologicInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
