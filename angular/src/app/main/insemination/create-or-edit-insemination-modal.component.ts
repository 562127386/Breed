import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { InseminationServiceProxy, InseminationCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { SpeciesInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';
import { format } from 'path';

@Component({
    selector: 'createOrEditInseminationModal',
    templateUrl: './create-or-edit-insemination-modal.component.html'
})
export class CreateOrEditInseminationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    @ViewChild('sexInfoCombobox', { static: true }) sexInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    @ViewChild('herdCombobox', { static: true }) herdCombobox: ElementRef;
    @ViewChild('activityInfoCombobox', { static: true }) activityInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    insemination: InseminationCreateOrUpdateInput = new InseminationCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];
    sexInfosSelectItems: SelectItem[] = [];
    herdsSelectItems: SelectItem[] = [];
    activityInfosSelectItems: SelectItem[] = [];
    breedInfosSelectItems: SelectItem[] = [];
    birthTypeInfosSelectItems: SelectItem[] = [];
    anomalyInfosSelectItems: SelectItem[] = [];
    membershipInfosSelectItems: SelectItem[] = [];
    bodyColorInfosSelectItems: SelectItem[] = [];
    spotConnectorInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    codeMask: string = '0';
    codePlaceHolder: string = '0';
    birthDateTemp: string;
    idIssueDateTemp: string;
    liveStockOfficer: string;
    liveStockNowDate: string;
    liveStockNowTime: string;
    herdId: number = undefined;
    activityInfoId: number = undefined;
    speciesInfoId: number = undefined;
    nationalCode: string = '';

    constructor(
        injector: Injector,
        private _inseminationService: InseminationServiceProxy,
        private _speciesInfoService: SpeciesInfoServiceProxy
    ) {
        super(injector);
    }

    show(inseminationId?: number,editdisabled?: boolean): void {  
        if (!inseminationId) {
            this.active = true;
            this.liveStockOfficer = this.appSession.user.name + ' ' + this.appSession.user.surname;
            this.liveStockNowDate = momentjalali().format('jYYYY/jMM/jDD');
            this.liveStockNowTime = momentjalali().format('HH:mm');
        }
        else{
            this.liveStockOfficer = '';
            this.liveStockNowDate = '';
            this.liveStockNowTime = '';
        }
        this.editdisabled = true;        
        this.codeMask = '999-9-99-9-99999999';

        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._inseminationService.getInseminationForEdit(inseminationId).subscribe(userResult => {
            this.insemination = userResult.insemination;
            
            this.birthDateTemp = this.getDate(userResult.insemination.birthDate);
            this.idIssueDateTemp = this.getDate(userResult.insemination.idIssueDate);

            this.speciesInfosSelectItems = _.map(userResult.speciesInfos, function(speciesInfo) {
                return {
                    label: speciesInfo.displayText, value: Number(speciesInfo.value)
                };
            });

            this.sexInfosSelectItems = _.map(userResult.sexInfos, function(sexInfo) {
                return {
                    label: sexInfo.displayText, value: Number(sexInfo.value)
                };
            });

            this.herdsSelectItems = _.map(userResult.herds, function(herd) {
                return {
                    label: herd.displayText, value: Number(herd.value)
                };
            });
            
            this.activityInfosSelectItems = _.map(userResult.activityInfos, function(activityInfo) {
                return {
                    label: activityInfo.displayText, value: Number(activityInfo.value)
                };
            });
            
            this.breedInfosSelectItems = _.map(userResult.breedInfos, function(breedInfo) {
                return {
                    label: breedInfo.displayText, value: Number(breedInfo.value)
                };
            });
            
            this.birthTypeInfosSelectItems = _.map(userResult.birthTypeInfos, function(birthTypeInfo) {
                return {
                    label: birthTypeInfo.displayText, value: Number(birthTypeInfo.value)
                };
            });
            
            this.anomalyInfosSelectItems = _.map(userResult.anomalyInfos, function(anomalyInfo) {
                return {
                    label: anomalyInfo.displayText, value: Number(anomalyInfo.value)
                };
            });
            
            this.membershipInfosSelectItems = _.map(userResult.membershipInfos, function(membershipInfo) {
                return {
                    label: membershipInfo.displayText, value: Number(membershipInfo.value)
                };
            });
            
            this.bodyColorInfosSelectItems = _.map(userResult.bodyColorInfos, function(bodyColorInfo) {
                return {
                    label: bodyColorInfo.displayText, value: Number(bodyColorInfo.value)
                };
            });
            
            this.spotConnectorInfosSelectItems = _.map(userResult.spotConnectorInfos, function(spotConnectorInfo) {
                return {
                    label: spotConnectorInfo.displayText, value: Number(spotConnectorInfo.value)
                };
            });

            if (inseminationId) {
                this.active = true;
                this.liveStockOfficer = userResult.insemination.officerName;
                this.liveStockNowDate = userResult.insemination.creationTime.format('YYYY/MM/DD');
                this.liveStockNowTime = userResult.insemination.creationTime.format('HH:mm');
            }
            this.getUserLocation();
            if(this.herdId != undefined){
                this.insemination.herdId = this.herdId;
                this.getActivities(this.herdId.toString());
                this.insemination.activityInfoId = this.activityInfoId;
                this.insemination.speciesInfoId = this.speciesInfoId;
                this.insemination.nationalCode = this.nationalCode;
                
            }
            this.modal.show();
        });
        
    }

    onShown(): void {        
    }

    save(shouldCountinue? : number): void {
        let input = new InseminationCreateOrUpdateInput();
        input = this.insemination;
        
        input.birthDate = this.setDate(this.birthDateTemp);
        input.idIssueDate = this.setDate(this.idIssueDateTemp);
        
        this.saving = true;
        this._inseminationService.createOrUpdateInsemination(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.insemination);
                if(shouldCountinue !== undefined && shouldCountinue == 1){
                    this.herdId = this.insemination.herdId;
                    this.activityInfoId = this.insemination.activityInfoId;
                    this.speciesInfoId = this.insemination.speciesInfoId;
                    let plaqueCode = Number(this.insemination.nationalCode);
                    if(plaqueCode < 1000000000){
                        this.nationalCode = (364052000000000+plaqueCode+1).toString();
                    }
                    else{
                        this.nationalCode = (plaqueCode+1).toString();
                    }

                    this.show();
                }
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    setCodeMask(speciesId : number): void {
        let speciesCode: string = '';
        this.codeMask = '0';
        this.codePlaceHolder = '0';
        this._speciesInfoService.getCodeRange(speciesId).subscribe(userResult => {
            this.codeMask = userResult;
            this.codePlaceHolder = userResult;            
        });
    }

    getActivities(activityInfoId: string): void { 

        this._inseminationService.getActivityForCombo(Number(activityInfoId)).subscribe(userResult => {
            
            this.activityInfosSelectItems = _.map(userResult, function(activityInfo) {
                return {
                    label: activityInfo.displayText, value: Number(activityInfo.value)
                };
            });
            this.insemination.activityInfoId = Number(userResult[0].value);
        });
        
    }

    getUserLocation(): void {
        // get Users current position    
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
            this.insemination.latitude = position.coords.latitude.toPrecision(9).toString();
            this.insemination.longitude = position.coords.longitude.toPrecision(9).toString();
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

    checkValidation(): void {                
        this._inseminationService.checkValidation(this.insemination).subscribe(userResult => {
            this.insemination = userResult;
        });
    }
}