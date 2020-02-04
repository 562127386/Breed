import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { VillageInfoServiceProxy, VillageInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditVillageInfoModal',
    templateUrl: './create-or-edit-villageInfo-modal.component.html'
})
export class CreateOrEditVillageInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    villageInfo: VillageInfoCreateOrUpdateInput = new VillageInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _villageInfoService: VillageInfoServiceProxy
    ) {
        super(injector);
    }

    show(villageInfoId?: number): void {
        if (!villageInfoId) {
            this.active = true;
        }

        this._villageInfoService.getVillageInfoForEdit(villageInfoId).subscribe(userResult => {
            this.villageInfo.name = userResult.name;
            this.villageInfo.code = userResult.code;
            this.villageInfo.id =  villageInfoId;

            if (villageInfoId) {
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
        this._villageInfoService.createOrUpdateVillageInfo(this.villageInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.villageInfo);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
