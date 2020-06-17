import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { SpotConnectorInfoServiceProxy, SpotConnectorInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditSpotConnectorInfoModal',
    templateUrl: './create-or-edit-spotConnectorInfo-modal.component.html'
})
export class CreateOrEditSpotConnectorInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    spotConnectorInfo: SpotConnectorInfoCreateOrUpdateInput = new SpotConnectorInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _spotConnectorInfoService: SpotConnectorInfoServiceProxy
    ) {
        super(injector);
    }

    show(spotConnectorInfoId?: number,editdisabled?: boolean): void {        
        if (!spotConnectorInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._spotConnectorInfoService.getSpotConnectorInfoForEdit(spotConnectorInfoId).subscribe(userResult => {
            this.spotConnectorInfo.name = userResult.name;
            this.spotConnectorInfo.code = userResult.code;
            this.spotConnectorInfo.id =  spotConnectorInfoId;

            if (spotConnectorInfoId) {
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
        this._spotConnectorInfoService.createOrUpdateSpotConnectorInfo(this.spotConnectorInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.spotConnectorInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
