import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ContractorServiceProxy, ContractorCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { VillageInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { CityInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { RegionInfoServiceProxy } from '@shared/service-proxies/service-proxies';
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
    @ViewChild('firmTypeCombobox', { static: true }) firmTypeCombobox: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    @ViewChild('regionInfoCombobox', { static: true }) regionInfoCombobox: ElementRef;
    @ViewChild('villageInfoCombobox', { static: true }) villageInfoCombobox: ElementRef;
    @ViewChild('unionInfoCombobox', { static: true }) unionInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    contractor: ContractorCreateOrUpdateInput = new ContractorCreateOrUpdateInput();    
    firmTypesSelectItems: SelectItem[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    cityInfosSelectItems: SelectItem[] = [];
    regionInfosSelectItems: SelectItem[] = [];
    villageInfosSelectItems: SelectItem[] = [];
    unionInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _contractorService: ContractorServiceProxy,
        private _villageInfoService: VillageInfoServiceProxy,
        private _cityInfoService : CityInfoServiceProxy,
        private _regionInfoService : RegionInfoServiceProxy
    ) {
        super(injector);
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

            this.regionInfosSelectItems = _.map(userResult.regionInfos, function(regionInfo) {
                return {
                    label: regionInfo.displayText, value: Number(regionInfo.value)
                };
            });
            
            this.villageInfosSelectItems = _.map(userResult.villageInfos, function(villageInfo) {
                return {
                    label: villageInfo.displayText, value: Number(villageInfo.value)
                };
            });

            this.unionInfosSelectItems = _.map(userResult.unionInfos, function(unionInfo) {
                return {
                    label: unionInfo.displayText, value: Number(unionInfo.value)
                };
            });

            if (contractorId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.nameInput.nativeElement.focus();
    }

    save(): void {
        let input = new ContractorCreateOrUpdateInput();
        input = this.contractor;
                
        input.birthDate = moment(this.contractor.birthDate.locale('en'));
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
    getCities(stateInfoId: string): void {  

        this._cityInfoService.getForCombo(Number(stateInfoId)).subscribe(userResult => {
            
            this.cityInfosSelectItems = _.map(userResult, function(cityInfo) {
                return {
                    label: cityInfo.displayText, value: Number(cityInfo.value)
                };
            });

        });
        this.regionInfosSelectItems = [];
        this.villageInfosSelectItems = [];
        this.contractor.institution = '';        
    }

    getRegions(cityInfoId: string): void {  

        this._regionInfoService.getForCombo(Number(cityInfoId)).subscribe(userResult => {
            
            this.regionInfosSelectItems = _.map(userResult, function(regionInfo) {
                return {
                    label: regionInfo.displayText, value: Number(regionInfo.value)
                };
            });

        });
        
        this.villageInfosSelectItems = [];
        this.contractor.institution = '';        
    }

    getVillages(regionInfoId: string): void {  

        this._villageInfoService.getForCombo(Number(regionInfoId)).subscribe(userResult => {
            
            this.villageInfosSelectItems = _.map(userResult, function(villageInfo) {
                return {
                    label: villageInfo.displayText, value: Number(villageInfo.value)
                };
            });

        });

        this.contractor.institution = '';
    }

    setInstitution(villageInfoId: string): void {

        this._villageInfoService.getCode(Number(villageInfoId)).subscribe(userResult => {            
            this.contractor.institution = userResult;
        });
    }

    setSubInstitution(subInstitution: string): void {

           this.contractor.subInstitution = subInstitution;
    }
}
