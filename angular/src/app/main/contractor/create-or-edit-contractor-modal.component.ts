import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ContractorEditDto, ContractorServiceProxy, ContractorCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';

@Component({
    selector: 'createOrEditContractorModal',
    templateUrl: './create-or-edit-contractor-modal.component.html'
})
export class CreateOrEditContractorModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('firmTypeCombobox', { static: true }) firmTypeCombobox: ElementRef;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    contractor: ContractorEditDto = new ContractorEditDto();
    firmTypesSelectItems: SelectItem[] = [];

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
            this.contractor = userResult.contractor;

            this.firmTypesSelectItems = _.map(userResult.firmTypes, function(firmType) {
                return {
                    label: firmType.displayText, value: firmType.value
                };
            });
            
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
        let input = new ContractorCreateOrUpdateInput();
        input = this.contractor;
        
        this.saving = true;
        this._contractorService.createOrUpdateContractor(input)
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
