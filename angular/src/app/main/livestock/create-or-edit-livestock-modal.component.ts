import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { LivestockServiceProxy, LivestockCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
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
    selector: 'createOrEditLivestockModal',
    templateUrl: './create-or-edit-livestock-modal.component.html'
})
export class CreateOrEditLivestockModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    @ViewChild('sexInfoCombobox', { static: true }) sexInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    @ViewChild('herdCombobox', { static: true }) herdCombobox: ElementRef;
    @ViewChild('activityInfoCombobox', { static: true }) activityInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    livestock: LivestockCreateOrUpdateInput = new LivestockCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];
    sexInfosSelectItems: SelectItem[] = [];
    herdsSelectItems: SelectItem[] = [];
    activityInfosSelectItems: SelectItem[] = [];
    sSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _livestockService: LivestockServiceProxy
    ) {
        super(injector);
    }

    show(livestockId?: number,editdisabled?: boolean): void {  
        if (!livestockId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._livestockService.getLivestockForEdit(livestockId).subscribe(userResult => {
            this.livestock = userResult.livestock;
            
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

            if (livestockId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.nameInput.nativeElement.focus();
    }

    save(): void {
        let input = new LivestockCreateOrUpdateInput();
        input = this.livestock;
        if(this.livestock.birthDate != undefined){        
            input.birthDate = moment(this.livestock.birthDate.locale('en'));
        }        
        this.saving = true;
        this._livestockService.createOrUpdateLivestock(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.livestock);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
