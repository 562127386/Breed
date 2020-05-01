import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HerdGeoLogServiceProxy, HerdGeoLogCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { SpeciesInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';
import { format } from 'path';

@Component({
    selector: 'createOrEditHerdGeoLogModal',
    templateUrl: './create-or-edit-herdGeoLog-modal.component.html'
})
export class CreateOrEditHerdGeoLogModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('herdCombobox', { static: true }) herdCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    herdGeoLog: HerdGeoLogCreateOrUpdateInput = new HerdGeoLogCreateOrUpdateInput();    
    herdsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    herdGeoLogOfficer: string;
    herdGeoLogNowDate: string;
    herdGeoLogNowTime: string;

    constructor(
        injector: Injector,
        private _herdGeoLogService: HerdGeoLogServiceProxy
    ) {
        super(injector);
    }

    show(herdGeoLogId?: number,editdisabled?: boolean): void {  
        if (!herdGeoLogId) {
            this.active = true;
            this.herdGeoLogOfficer = this.appSession.user.name + ' ' + this.appSession.user.surname;
            this.herdGeoLogNowDate = momentjalali().format('jYYYY/jMM/jDD');
            this.herdGeoLogNowTime = momentjalali().format('HH:mm');
        }
        else{
            this.herdGeoLogOfficer = '';
            this.herdGeoLogNowDate = '';
            this.herdGeoLogNowTime = '';
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._herdGeoLogService.getHerdGeoLogForEdit(herdGeoLogId).subscribe(userResult => {
            this.herdGeoLog = userResult.herdGeoLog;                

            this.herdsSelectItems = _.map(userResult.herds, function(herd) {
                return {
                    label: herd.displayText, value: Number(herd.value)
                };
            });

            if (herdGeoLogId) {
                this.active = true;
            }
            this.getUserLocation();
            this.modal.show();
        });
        
    }

    onShown(): void {        
    }

    save(): void {
        let input = new HerdGeoLogCreateOrUpdateInput();
        input = this.herdGeoLog;
        
        this.saving = true;
        this._herdGeoLogService.createOrUpdateHerdGeoLog(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.herdGeoLog);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    getUserLocation(): void {
        // get Users current position    
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(position => {
            this.herdGeoLog.latitude = position.coords.latitude.toPrecision(9).toString();
            this.herdGeoLog.longitude = position.coords.longitude.toPrecision(9).toString();
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