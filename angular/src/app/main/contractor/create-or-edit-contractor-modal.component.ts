import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ContractorServiceProxy, ContractorCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditContractorModal',
    templateUrl: './create-or-edit-contractor-modal.component.html'
})
export class CreateOrEditContractorModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    contractor: ContractorCreateOrUpdateInput = new ContractorCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _contractorService: ContractorServiceProxy
    ) {
        super(injector);
    }

    show(contractorId?: number): void {
        if (!contractorId) {
            this.active = true;
        }

        this._contractorService.getContractorForEdit(contractorId).subscribe(userResult => {
            this.contractor.name = userResult.name;
            this.contractor.code = userResult.code;
            this.contractor.id =  contractorId;

            if (contractorId) {
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
        this._contractorService.createOrUpdateContractor(this.contractor)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.contractor);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
