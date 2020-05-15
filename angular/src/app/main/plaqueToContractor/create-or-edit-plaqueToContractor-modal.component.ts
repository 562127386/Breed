import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueToContractorServiceProxy, PlaqueToContractorCreateOrUpdateInput, ContractorServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditPlaqueToContractorModal',
    templateUrl: './create-or-edit-plaqueToContractor-modal.component.html'
})
export class CreateOrEditPlaqueToContractorModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('contractorCombobox', { static: true }) contractorCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueToContractor: PlaqueToContractorCreateOrUpdateInput = new PlaqueToContractorCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    contractorsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    setTimeTemp: string;

    constructor(
        injector: Injector,
        private _plaqueToContractorService: PlaqueToContractorServiceProxy,
        private _contractorService : ContractorServiceProxy
    ) {
        super(injector);
    }

    show(plaqueToContractorId?: number,editdisabled?: boolean): void {  
        if (!plaqueToContractorId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueToContractorService.getPlaqueToContractorForEdit(plaqueToContractorId).subscribe(userResult => {
            this.plaqueToContractor = userResult.plaqueToContractor;
            
            this.setTimeTemp = this.getDate(userResult.plaqueToContractor.setTime);

            this.speciesInfosSelectItems = _.map(userResult.speciesInfos, function(speciesInfo) {
                return {
                    label: speciesInfo.displayText, value: Number(speciesInfo.value)
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

            if (plaqueToContractorId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        let input = new PlaqueToContractorCreateOrUpdateInput();
        input = this.plaqueToContractor;
        this.saving = true;        
        
        input.setTime = this.setDate(this.setTimeTemp);
        
        this._plaqueToContractorService.createOrUpdatePlaqueToContractor(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueToContractor);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    getContractors(stateInfoId: string): void {  

        this._contractorService.getForCombo(Number(stateInfoId)).subscribe(userResult => {
            
            this.contractorsSelectItems = _.map(userResult, function(contractor) {
                return {
                    label: contractor.displayText, value: Number(contractor.value)
                };
            });

        });
        
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
