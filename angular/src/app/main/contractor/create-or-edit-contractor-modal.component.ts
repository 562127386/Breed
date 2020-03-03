import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ContractorServiceProxy, ContractorCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentj from 'jalali-moment';

@Component({
    selector: 'createOrEditContractorModal',
    templateUrl: './create-or-edit-contractor-modal.component.html'
})
export class CreateOrEditContractorModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    contractor: ContractorCreateOrUpdateInput = new ContractorCreateOrUpdateInput();    
    firmTypesSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    date;

    constructor(
        injector: Injector,
        private _contractorService: ContractorServiceProxy
    ) {
        super(injector);
        this.date = momentj('2020-03-17', 'YYYY/MM/DD');
    }

    show(contractorId?: number,editdisabled?: boolean): void {  
        if (!contractorId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._contractorService.getContractorForEdit(contractorId).subscribe(userResult => {
            this.contractor = userResult.contractor;
            
            this.firmTypesSelectItems = _.map(userResult.firmTypes, function(firmType) {
                return {
                    label: firmType.displayText, value: Number(firmType.value)
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
        
        this.date = momentj('2020-03-17', 'YYYY/MM/DD');
        input.birthDate = moment('2020-03-17');
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
