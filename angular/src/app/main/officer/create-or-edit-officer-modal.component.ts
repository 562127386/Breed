import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { OfficerServiceProxy, OfficerCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';

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

    officer: OfficerCreateOrUpdateInput = new OfficerCreateOrUpdateInput();    
    academicDegreesSelectItems: SelectItem[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    contractorsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    birthDateTemp: string;

    constructor(
        injector: Injector,
        private _officerService: OfficerServiceProxy
    ) {
        super(injector);
    }

    show(officerId?: number,editdisabled?: boolean): void {  
        if (!officerId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._officerService.getOfficerForEdit(officerId).subscribe(userResult => {
            this.officer = userResult.officer;
            
            this.birthDateTemp = this.getDate(userResult.officer.birthDate);

            this.academicDegreesSelectItems = _.map(userResult.academicDegrees, function(academicDegree) {
                return {
                    label: academicDegree.displayText, value: Number(academicDegree.value)
                };
            });

            this.stateInfosSelectItems = _.map(userResult.stateInfos, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            this.contractorsSelectItems = _.map(userResult.contractors, function(contractor) {
                return {
                    label: contractor.displayText, value: Number(contractor.value)
                };
            });

            if (officerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.nameInput.nativeElement.focus();
    }

    save(): void {
        let input = new OfficerCreateOrUpdateInput();
        input = this.officer;
                        
        input.birthDate = this.setDate(this.birthDateTemp);           
        
        this.saving = true;
        this._officerService.createOrUpdateOfficer(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.officer);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    getDate(input: moment.Moment): string {
        if( input !== undefined){
            return input.format('YYYY/MM/DD');
        }
        return '';
    }

    setDate(input: string): moment.Moment {
        if( input !== undefined && input != ''){
            return moment(input);
        }
        return undefined;
    }
}
