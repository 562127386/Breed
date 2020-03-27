import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HerdServiceProxy, HerdCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
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
    epidemiologicInfosSelectItems: SelectItem[] = [];
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

    constructor(
        injector: Injector,
        private _herdService: HerdServiceProxy,
        private _villageInfoService: VillageInfoServiceProxy,
        private _cityInfoService : CityInfoServiceProxy,
        private _regionInfoService : RegionInfoServiceProxy
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
            
            this.epidemiologicInfosSelectItems = _.map(userResult.epidemiologicInfos, function(epidemiologicInfo) {
                return {
                    label: epidemiologicInfo.displayText, value: Number(epidemiologicInfo.value)
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
        let input = new HerdCreateOrUpdateInput();
        input = this.herd;
        if(this.herd.birthDate != undefined){        
            input.birthDate = moment(this.herd.birthDate.locale('en'));
        }
        if(this.herd.issueDate != undefined){
            input.issueDate = moment(this.herd.issueDate.locale('en'));
        }
        if(this.herd.validityDate != undefined){
            input.validityDate = moment(this.herd.validityDate.locale('en'));
        }
        this.saving = true;
        this._herdService.createOrUpdateHerd(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.herd);
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
        this.regionInfosSelectItems = [];
        this.villageInfosSelectItems = [];
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
        this.herd.institution = '';        
    }

    getVillages(regionInfoId: string): void {  

        this._villageInfoService.getForCombo(Number(regionInfoId)).subscribe(userResult => {
            
            this.villageInfosSelectItems = _.map(userResult, function(villageInfo) {
                return {
                    label: villageInfo.displayText, value: Number(villageInfo.value)
                };
            });

        });

        this.herd.institution = '';
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
            this.herd.latitude = position.coords.latitude.toString();
            this.herd.longitude = position.coords.longitude.toString();
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
}
