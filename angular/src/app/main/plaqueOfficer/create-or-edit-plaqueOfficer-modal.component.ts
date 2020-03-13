import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueOfficerServiceProxy, PlaqueOfficerCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';

@Component({
    selector: 'createOrEditPlaqueOfficerModal',
    templateUrl: './create-or-edit-plaqueOfficer-modal.component.html'
})
export class CreateOrEditPlaqueOfficerModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;    
    @ViewChild('plaqueStoreCombobox', { static: true }) plaqueStoreCombobox: ElementRef;
    @ViewChild('officerCombobox', { static: true }) officerCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueOfficer: PlaqueOfficerCreateOrUpdateInput = new PlaqueOfficerCreateOrUpdateInput();    
    plaqueStoresSelectItems: SelectItem[] = [];
    officersSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _plaqueOfficerService: PlaqueOfficerServiceProxy
    ) {
        super(injector);
    }

    show(plaqueOfficerId?: number,editdisabled?: boolean): void {  
        if (!plaqueOfficerId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueOfficerService.getPlaqueOfficerForEdit(plaqueOfficerId).subscribe(userResult => {
            this.plaqueOfficer = userResult.plaqueOfficer;
            
            this.plaqueStoresSelectItems = _.map(userResult.plaqueStores, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            this.officersSelectItems = _.map(userResult.officers, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            if (plaqueOfficerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        let input = new PlaqueOfficerCreateOrUpdateInput();
        input = this.plaqueOfficer;

        this.saving = true;
        this._plaqueOfficerService.createOrUpdatePlaqueOfficer(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueOfficer);
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
