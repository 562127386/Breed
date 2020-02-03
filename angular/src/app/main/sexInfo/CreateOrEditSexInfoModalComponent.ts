import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { SexInfoServiceProxy, SexInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
@Component({
    selector: 'createOrEditSexInfoModal',
    templateUrl: './create-or-edit-sexInfo-modal.component.html'
})
export class CreateOrEditSexInfoModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true })
    modal: ModalDirective;
    @Output()
    modalSave: EventEmitter<any> = new EventEmitter<any>();
    sexInfo: SexInfoCreateOrUpdateInput = new SexInfoCreateOrUpdateInput();
    active: boolean = false;
    saving: boolean = false;
    constructor(injector: Injector, private _sexInfoService: SexInfoServiceProxy) {
        super(injector);
    }
    show(sexInfoId?: number): void {
        if (!sexInfoId) {
            this.active = true;
        }
        this._sexInfoService.getSexInfoForEdit(sexInfoId).subscribe(userResult => {
            this.sexInfo.name = userResult.name;
            this.sexInfo.code = userResult.code;
            this.sexInfo.id = sexInfoId;
            if (sexInfoId) {
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
        this.modal.hide();
    }
}
