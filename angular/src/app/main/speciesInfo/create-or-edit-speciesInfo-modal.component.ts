import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { SpeciesInfoServiceProxy, SpeciesInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditSpeciesInfoModal',
    templateUrl: './create-or-edit-speciesInfo-modal.component.html'
})
export class CreateOrEditSpeciesInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    speciesInfo: SpeciesInfoCreateOrUpdateInput = new SpeciesInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _speciesInfoService: SpeciesInfoServiceProxy
    ) {
        super(injector);
    }

    show(speciesInfoId?: number): void {
        if (!speciesInfoId) {
            this.active = true;
        }

        this._speciesInfoService.getSpeciesInfoForEdit(speciesInfoId).subscribe(userResult => {
            this.speciesInfo.name = userResult.name;
            this.speciesInfo.code = userResult.code;
            this.speciesInfo.id =  speciesInfoId;

            if (speciesInfoId) {
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
        this._speciesInfoService.createOrUpdateSpeciesInfo(this.speciesInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.speciesInfo);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
