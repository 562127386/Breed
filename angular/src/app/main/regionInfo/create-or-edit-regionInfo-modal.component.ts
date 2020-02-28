import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { RegionInfoServiceProxy, RegionInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { CityInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';

@Component({
    selector: 'createOrEditRegionInfoModal',
    templateUrl: './create-or-edit-regionInfo-modal.component.html'
})
export class CreateOrEditRegionInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @ViewChild('cityInfoCombobox', { static: true }) cityInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    regionInfo: RegionInfoCreateOrUpdateInput = new RegionInfoCreateOrUpdateInput();    
    stateInfosSelectItems: SelectItem[] = [];
    cityInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _regionInfoService: RegionInfoServiceProxy,
        private _cityInfoService : CityInfoServiceProxy
    ) {
        super(injector);
    }

    show(regionInfoId?: number,editdisabled?: boolean): void {  
        if (!regionInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._regionInfoService.getRegionInfoForEdit(regionInfoId).subscribe(userResult => {
            this.regionInfo = userResult.regionInfo;
            
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

            if (regionInfoId) {
                this.active = true;
            }

            this.modal.show();

        });
        
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

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        let input = new RegionInfoCreateOrUpdateInput();
        input = this.regionInfo;

        this.saving = true;
        this._regionInfoService.createOrUpdateRegionInfo(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.regionInfo);
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
}
