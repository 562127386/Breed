import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueToHerdServiceProxy, PlaqueToHerdCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';
import { format } from 'path';

@Component({
    selector: 'createOrEditPlaqueToHerdModal',
    templateUrl: './create-or-edit-plaqueToHerd-modal.component.html'
})
export class CreateOrEditPlaqueToHerdModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('herdCombobox', { static: true }) herdCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueToHerd: PlaqueToHerdCreateOrUpdateInput = new PlaqueToHerdCreateOrUpdateInput();    
    herdsSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    codeMask: string = '0';
    codePlaceHolder: string = '0';
    liveStockOfficer: string;
    liveStockNowDate: string;
    liveStockNowTime: string;

    constructor(
        injector: Injector,
        private _plaqueToHerdService: PlaqueToHerdServiceProxy
    ) {
        super(injector);
    }

    show(plaqueToHerdId?: number,editdisabled?: boolean): void {  
        if (!plaqueToHerdId) {
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
        this.codeMask = '364-0-52-9-99999999';

        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueToHerdService.getPlaqueToHerdForEdit(plaqueToHerdId).subscribe(userResult => {
            this.plaqueToHerd = userResult.plaqueToHerd;
            
            this.herdsSelectItems = _.map(userResult.herds, function(herd) {
                return {
                    label: herd.displayText, value: Number(herd.value)
                };
            });

            if (plaqueToHerdId) {
                this.active = true;
                this.liveStockOfficer = userResult.plaqueToHerd.officerName;
                this.liveStockNowDate = userResult.plaqueToHerd.creationTime.format('YYYY/MM/DD');
                this.liveStockNowTime = userResult.plaqueToHerd.creationTime.format('HH:mm');
            }
            this.getUserLocation();
            this.modal.show();
        });
        
    }

    onShown(): void {        
    }

    save(shouldCountinue? : number): void {
        let input = new PlaqueToHerdCreateOrUpdateInput();
        input = this.plaqueToHerd;
        
        this.saving = true;
        this._plaqueToHerdService.createOrUpdatePlaqueToHerd(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueToHerd);
                if(shouldCountinue !== undefined && shouldCountinue == 1){                
                    this.show();
                }
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
            this.plaqueToHerd.latitude = position.coords.latitude.toString();
            this.plaqueToHerd.longitude = position.coords.longitude.toString();
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
        this._plaqueToHerdService.checkValidation(this.plaqueToHerd).subscribe(userResult => {
            this.plaqueToHerd = userResult;
        });
    }
}