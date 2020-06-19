import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { BirthTypeInfoServiceProxy, BirthTypeInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditBirthTypeInfoModal',
    templateUrl: './create-or-edit-birthTypeInfo-modal.component.html'
})
export class CreateOrEditBirthTypeInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    birthTypeInfo: BirthTypeInfoCreateOrUpdateInput = new BirthTypeInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _birthTypeInfoService: BirthTypeInfoServiceProxy
    ) {
        super(injector);
    }

    show(birthTypeInfoId?: number,editdisabled?: boolean): void {        
        if (!birthTypeInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._birthTypeInfoService.getBirthTypeInfoForEdit(birthTypeInfoId).subscribe(userResult => {
            this.birthTypeInfo.name = userResult.name;
            this.birthTypeInfo.code = userResult.code;
            this.birthTypeInfo.id =  birthTypeInfoId;

            if (birthTypeInfoId) {
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
        this._birthTypeInfoService.createOrUpdateBirthTypeInfo(this.birthTypeInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.birthTypeInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
