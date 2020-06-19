import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { BodyColorInfoServiceProxy, BodyColorInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditBodyColorInfoModal',
    templateUrl: './create-or-edit-bodyColorInfo-modal.component.html'
})
export class CreateOrEditBodyColorInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    bodyColorInfo: BodyColorInfoCreateOrUpdateInput = new BodyColorInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _bodyColorInfoService: BodyColorInfoServiceProxy
    ) {
        super(injector);
    }

    show(bodyColorInfoId?: number,editdisabled?: boolean): void {        
        if (!bodyColorInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._bodyColorInfoService.getBodyColorInfoForEdit(bodyColorInfoId).subscribe(userResult => {
            this.bodyColorInfo.name = userResult.name;
            this.bodyColorInfo.code = userResult.code;
            this.bodyColorInfo.id =  bodyColorInfoId;

            if (bodyColorInfoId) {
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
        this._bodyColorInfoService.createOrUpdateBodyColorInfo(this.bodyColorInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.bodyColorInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
