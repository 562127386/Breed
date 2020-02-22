import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { StateInfoServiceProxy, StateInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditStateInfoModal',
    templateUrl: './create-or-edit-stateInfo-modal.component.html'
})
export class CreateOrEditStateInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    stateInfo: StateInfoCreateOrUpdateInput = new StateInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _stateInfoService: StateInfoServiceProxy
    ) {
        super(injector);
    }

    show(stateInfoId?: number): void {
        if (!stateInfoId) {
            this.active = true;
        }
        this._stateInfoService.getStateInfoForEdit(stateInfoId).subscribe(userResult => {
            this.stateInfo.name = userResult.name;
            this.stateInfo.code = userResult.code;
            this.stateInfo.id =  stateInfoId;

            if (stateInfoId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._stateInfoService.createOrUpdateStateInfo(this.stateInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.stateInfo);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
