import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ManufacturerServiceProxy, ManufacturerCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditManufacturerModal',
    templateUrl: './create-or-edit-manufacturer-modal.component.html'
})
export class CreateOrEditManufacturerModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    manufacturer: ManufacturerCreateOrUpdateInput = new ManufacturerCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _manufacturerService: ManufacturerServiceProxy
    ) {
        super(injector);
    }

    show(manufacturerId?: number,editdisabled?: boolean): void {        
        if (!manufacturerId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._manufacturerService.getManufacturerForEdit(manufacturerId).subscribe(userResult => {
            this.manufacturer.name = userResult.name;
            this.manufacturer.code = userResult.code;
            this.manufacturer.isImporter = userResult.isImporter;
            this.manufacturer.id =  manufacturerId;

            if (manufacturerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._manufacturerService.createOrUpdateManufacturer(this.manufacturer)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.manufacturer);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
