import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueToOfficerServiceProxy, PlaqueToOfficerCreateOrUpdateInput, CityInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditPlaqueToOfficerModal',
    templateUrl: './create-or-edit-plaqueToOfficer-modal.component.html'
})
export class CreateOrEditPlaqueToOfficerModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    @ViewChild('officerCombobox', { static: true }) officerCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueToOfficer: PlaqueToOfficerCreateOrUpdateInput = new PlaqueToOfficerCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    cityInfosSelectItems: SelectItem[] = [];
    officersSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    setTimeTemp: string;

    constructor(
        injector: Injector,
        private _plaqueToOfficerService: PlaqueToOfficerServiceProxy,
        private _cityInfoService : CityInfoServiceProxy
    ) {
        super(injector);
    }

    show(plaqueToOfficerId?: number,editdisabled?: boolean): void {  
        if (!plaqueToOfficerId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueToOfficerService.getPlaqueToOfficerForEdit(plaqueToOfficerId).subscribe(userResult => {
            this.plaqueToOfficer = userResult.plaqueToOfficer;
            
            this.setTimeTemp = this.getDate(userResult.plaqueToOfficer.setTime);

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

            this.cityInfosSelectItems = _.map(userResult.cityInfos, function(cityInfo) {
                return {
                    label: cityInfo.displayText, value: Number(cityInfo.value)
                };
            });

            this.officersSelectItems = _.map(userResult.officers, function(officer) {
                return {
                    label: officer.displayText, value: Number(officer.value)
                };
            });

            if (plaqueToOfficerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        let input = new PlaqueToOfficerCreateOrUpdateInput();
        input = this.plaqueToOfficer;
        this.saving = true;        
        
        input.setTime = this.setDate(this.setTimeTemp);
        
        this._plaqueToOfficerService.createOrUpdatePlaqueToOfficer(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueToOfficer);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    getCities(stateInfoId: string): void {  

        this._cityInfoService.getForCombo(Number(stateInfoId)).subscribe(userResult => {
            
            this.cityInfosSelectItems = _.map(userResult, function(cityInfo) {
                return {
                    label: cityInfo.displayText, value: Number(cityInfo.value)
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
        if( input !== undefined || input != ''){
            return moment(input);
        }
        return undefined;
    }

}
