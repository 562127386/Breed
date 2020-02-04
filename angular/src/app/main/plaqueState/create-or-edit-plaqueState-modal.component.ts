import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueStateServiceProxy, PlaqueStateCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditPlaqueStateModal',
    templateUrl: './create-or-edit-plaqueState-modal.component.html'
})
export class CreateOrEditPlaqueStateModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueState: PlaqueStateCreateOrUpdateInput = new PlaqueStateCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _plaqueStateService: PlaqueStateServiceProxy
    ) {
        super(injector);
    }

    show(plaqueStateId?: number): void {
        if (!plaqueStateId) {
            this.active = true;
        }

        this._plaqueStateService.getPlaqueStateForEdit(plaqueStateId).subscribe(userResult => {
            this.plaqueState.name = userResult.name;
            this.plaqueState.code = userResult.code;
            this.plaqueState.id =  plaqueStateId;

            if (plaqueStateId) {
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
        this._plaqueStateService.createOrUpdatePlaqueState(this.plaqueState)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueState);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
