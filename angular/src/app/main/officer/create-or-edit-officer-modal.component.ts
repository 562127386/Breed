import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { OfficerEditDto, OfficerServiceProxy, OfficerCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';

@Component({
    selector: 'createOrEditOfficerModal',
    templateUrl: './create-or-edit-officer-modal.component.html'
})
export class CreateOrEditOfficerModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('academicDegreeCombobox', { static: true }) academicDegreeCombobox: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('contractorCombobox', { static: true }) contractorCombobox: ElementRef;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    officer: OfficerEditDto = new OfficerEditDto();
    academicDegreesSelectItems: SelectItem[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    contractorsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _officerService: OfficerServiceProxy
    ) {
        super(injector);
    }

    show(officerId?: number): void {
        if (!officerId) {
            this.active = true;
        }

        this._officerService.getOfficerForEdit(officerId).subscribe(userResult => {
            this.officer = userResult.officer;

            this.academicDegreesSelectItems = _.map(userResult.academicDegrees, function(academicDegree) {
                return {
                    label: academicDegree.displayText, value: academicDegree.value
                };
            });

            this.stateInfosSelectItems = _.map(userResult.stateInfos, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: stateInfo.value
                };
            });

            this.contractorsSelectItems = _.map(userResult.contractors, function(contractor) {
                return {
                    label: contractor.displayText, value: contractor.value
                };
            });
            
            if (officerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        let input = new OfficerCreateOrUpdateInput();
        input = this.officer;
        
        this.saving = true;
        this._officerService.createOrUpdateOfficer(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(input);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
