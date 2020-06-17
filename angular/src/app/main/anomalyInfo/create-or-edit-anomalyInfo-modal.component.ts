import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { AnomalyInfoServiceProxy, AnomalyInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditAnomalyInfoModal',
    templateUrl: './create-or-edit-anomalyInfo-modal.component.html'
})
export class CreateOrEditAnomalyInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    anomalyInfo: AnomalyInfoCreateOrUpdateInput = new AnomalyInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _anomalyInfoService: AnomalyInfoServiceProxy
    ) {
        super(injector);
    }

    show(anomalyInfoId?: number,editdisabled?: boolean): void {        
        if (!anomalyInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._anomalyInfoService.getAnomalyInfoForEdit(anomalyInfoId).subscribe(userResult => {
            this.anomalyInfo.name = userResult.name;
            this.anomalyInfo.code = userResult.code;
            this.anomalyInfo.id =  anomalyInfoId;

            if (anomalyInfoId) {
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
        this._anomalyInfoService.createOrUpdateAnomalyInfo(this.anomalyInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.anomalyInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
