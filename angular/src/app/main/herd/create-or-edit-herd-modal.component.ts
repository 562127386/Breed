import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HerdServiceProxy, HerdCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { VillageInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { CityInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { RegionInfoServiceProxy, ContractorServiceProxy, UnionInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditHerdModal',
    templateUrl: './create-or-edit-herd-modal.component.html'
})
export class CreateOrEditHerdModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('epidemiologicInfoCombobox', { static: true }) epidemiologicInfoCombobox: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    @ViewChild('regionInfoCombobox', { static: true }) regionInfoCombobox: ElementRef;
    @ViewChild('villageInfoCombobox', { static: true }) villageInfoCombobox: ElementRef;
    @ViewChild('unionInfoCombobox', { static: true }) unionInfoCombobox: ElementRef;
    @ViewChild('activityInfoCombobox', { static: true }) activityInfoCombobox: ElementRef;
    @ViewChild('contractorCombobox', { static: true }) contractorCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    herd: HerdCreateOrUpdateInput = new HerdCreateOrUpdateInput();    
    stateInfosSelectItems: SelectItem[] = [];
    cityInfosSelectItems: SelectItem[] = [];
    regionInfosSelectItems: SelectItem[] = [];
    villageInfosSelectItems: SelectItem[] = [];
    unionInfosSelectItems: SelectItem[] = [];
    activityInfosSelectItems: SelectItem[] = [];
    contractorsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    nationalCodeValid: boolean = true;
    birthDateTemp: string;
    issueDateTemp: string;
    validityDateTemp: string;

    constructor(
        injector: Injector,
        private _herdService: HerdServiceProxy,
        private _villageInfoService: VillageInfoServiceProxy,
        private _cityInfoService : CityInfoServiceProxy,
        private _regionInfoService : RegionInfoServiceProxy,
        private _unionInfoService : UnionInfoServiceProxy,
        private _contractorService : ContractorServiceProxy
    ) {
        super(injector);
    }

    show(herdId?: number,editdisabled?: boolean): void {  
        if (!herdId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._herdService.getHerdForEdit(herdId).subscribe(userResult => {
            this.herd = userResult.herd;

            this.birthDateTemp = this.getDate(userResult.herd.birthDate);
            this.issueDateTemp = this.getDate(userResult.herd.issueDate);
            this.validityDateTemp = this.getDate(userResult.herd.validityDate);


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

            this.activityInfosSelectItems = _.map(userResult.activityInfos, function(activityInfo) {
                return {
                    label: activityInfo.displayText, value: Number(activityInfo.value)
                };
            });

            this.contractorsSelectItems = _.map(userResult.contractors, function(contractor) {
                return {
                    label: contractor.displayText, value: Number(contractor.value)
                };
            });

            if (herdId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.nameInput.nativeElement.focus();
    }

    save(): void {
        if(this.nationalCodeValid || !this.herd.iranian || !this.herd.reality){
            let input = new HerdCreateOrUpdateInput();
            input = this.herd;

            input.birthDate = this.setDate(this.birthDateTemp);   
            input.issueDate = this.setDate(this.issueDateTemp); 
            input.validityDate = this.setDate(this.validityDateTemp); 

            this.saving = true;
            this._herdService.createOrUpdateHerd(input)
                .pipe(finalize(() => this.saving = false))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(this.herd);
                });
        }
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
        this._unionInfoService.getForCombo(Number(stateInfoId)).subscribe(userResult => {
            
            this.unionInfosSelectItems = _.map(userResult, function(unionInfo) {
                return {
                    label: unionInfo.displayText, value: Number(unionInfo.value)
                };
            });

        });
        this._contractorService.getForCombo(Number(stateInfoId)).subscribe(userResult => {
            
            this.contractorsSelectItems = _.map(userResult, function(contractor) {
                return {
                    label: contractor.displayText, value: Number(contractor.value)
                };
            });

        });   
        this.regionInfosSelectItems = [];
        this.villageInfosSelectItems = [];        
        this.contractorsSelectItems = [];
        this.herd.institution = '';        
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
        this._cityInfoService.getCode(Number(cityInfoId)).subscribe(userResult => {            
            this.herd.institution = userResult;
        });       
    }

    getVillages(regionInfoId: string): void {  

        this._villageInfoService.getForCombo(Number(regionInfoId)).subscribe(userResult => {
            
            this.villageInfosSelectItems = _.map(userResult, function(villageInfo) {
                return {
                    label: villageInfo.displayText, value: Number(villageInfo.value)
                };
            });

        });

        this._regionInfoService.getCode(Number(regionInfoId)).subscribe(userResult => {            
            this.herd.institution = userResult;
        });
    }

    setInstitution(villageInfoId: string): void {

        this._villageInfoService.getCode(Number(villageInfoId)).subscribe(userResult => {            
            this.herd.institution = userResult;
        });
    }

    getUserLocation(): void {
        // get Users current position    
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
            this.herd.latitude = position.coords.latitude.toPrecision(9).toString();
            this.herd.longitude = position.coords.longitude.toPrecision(9).toString();
            console.log("position", position)
            }, error => {
              //Handle Errors
                switch(error.code) {
                    case error.PERMISSION_DENIED:
                        console.log("User denied the request for Geolocation.");
                        break;
                    case error.POSITION_UNAVAILABLE:
                        console.log("Location information is unavailable.");
                        break;
                    case error.TIMEOUT:
                        console.log("The request to get user location timed out.");
                        break;
                    default:
                        console.log("An unknown error occurred.");
                        break;
                }
            });            
        }
        else{
            // container.innerHTML = "Geolocation is not Supported for this browser/OS.";
            alert("Geolocation is not Supported for this browser/OS");
        }        
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

    checkNationalCode(): void {
        let meli_code = this.herd.nationalCode;
        if (meli_code.length == 12) {
            if (meli_code == '111-111111-1' ||
                meli_code == '000-000000-0' ||
                meli_code == '222-222222-2' ||
                meli_code == '333-333333-3' ||
                meli_code == '444-444444-4' ||
                meli_code == '555-555555-5' ||
                meli_code == '666-666666-6' ||
                meli_code == '777-777777-7' ||
                meli_code == '888-888888-8' ||
                meli_code == '999-999999-9') {
                    this.nationalCodeValid = false;
            } else {
                let c = parseInt(meli_code.charAt(11));
                let n = parseInt(meli_code.charAt(0)) * 10 +
                    parseInt(meli_code.charAt(1)) * 9 +
                    parseInt(meli_code.charAt(2)) * 8 +
                    parseInt(meli_code.charAt(4)) * 7 +
                    parseInt(meli_code.charAt(5)) * 6 +
                    parseInt(meli_code.charAt(6)) * 5 +
                    parseInt(meli_code.charAt(7)) * 4 +
                    parseInt(meli_code.charAt(8)) * 3 +
                    parseInt(meli_code.charAt(9)) * 2;                  
                let r = n % 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r)) {
                    this.nationalCodeValid = true;
                } else {
                    this.nationalCodeValid = false;
                }
            }
        } else {
            this.nationalCodeValid = false;
        }
    }
}
