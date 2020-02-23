import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { SexInfoServiceProxy, SexInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditSexInfoModal',
    templateUrl: './create-or-edit-sexInfo-modal.component.html'
})
export class CreateOrEditSexInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    sexInfo: SexInfoCreateOrUpdateInput = new SexInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _sexInfoService: SexInfoServiceProxy
    ) {
        super(injector);
    }

    show(sexInfoId?: number,editdisabled?: boolean): void {        
        if (!sexInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._sexInfoService.getSexInfoForEdit(sexInfoId).subscribe(userResult => {
            this.sexInfo.name = userResult.name;
            this.sexInfo.code = userResult.code;
            this.sexInfo.id =  sexInfoId;

            if (sexInfoId) {
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
        this._sexInfoService.createOrUpdateSexInfo(this.sexInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.sexInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    numberOnly(event): boolean {
        const charCode = (event.which) ? event.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
  }
}
