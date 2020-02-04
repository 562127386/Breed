import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { CityInfoServiceProxy, CityInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditCityInfoModal',
    templateUrl: './create-or-edit-cityInfo-modal.component.html'
})
export class CreateOrEditCityInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    cityInfo: CityInfoCreateOrUpdateInput = new CityInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _cityInfoService: CityInfoServiceProxy
    ) {
        super(injector);
    }

    show(cityInfoId?: number): void {
        if (!cityInfoId) {
            this.active = true;
        }

        this._cityInfoService.getCityInfoForEdit(cityInfoId).subscribe(userResult => {
            this.cityInfo.name = userResult.name;
            this.cityInfo.code = userResult.code;
            this.cityInfo.id =  cityInfoId;

            if (cityInfoId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._cityInfoService.createOrUpdateCityInfo(this.cityInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.cityInfo);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
