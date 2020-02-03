import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ProviderInfoServiceProxy, ProviderInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditProviderInfoModal',
    templateUrl: './create-or-edit-providerInfo-modal.component.html'
})
export class CreateOrEditProviderInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    providerInfo: ProviderInfoCreateOrUpdateInput = new ProviderInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _providerInfoService: ProviderInfoServiceProxy
    ) {
        super(injector);
    }

    show(providerInfoId?: number): void {
        if (!providerInfoId) {
            this.active = true;
        }

        this._providerInfoService.getProviderInfoForEdit(providerInfoId).subscribe(userResult => {
            this.providerInfo.name = userResult.name;
            this.providerInfo.code = userResult.code;
            this.providerInfo.id =  providerInfoId;

            if (providerInfoId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        document.getElementById('Name').focus();
    }

    save(): void {
        this.saving = true;
        this._providerInfoService.createOrUpdateProviderInfo(this.providerInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.providerInfo);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
