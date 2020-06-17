import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { BreedInfoServiceProxy, BreedInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditBreedInfoModal',
    templateUrl: './create-or-edit-breedInfo-modal.component.html'
})
export class CreateOrEditBreedInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    breedInfo: BreedInfoCreateOrUpdateInput = new BreedInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _breedInfoService: BreedInfoServiceProxy
    ) {
        super(injector);
    }

    show(breedInfoId?: number,editdisabled?: boolean): void {        
        if (!breedInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._breedInfoService.getBreedInfoForEdit(breedInfoId).subscribe(userResult => {
            this.breedInfo.name = userResult.name;
            this.breedInfo.code = userResult.code;
            this.breedInfo.id =  breedInfoId;

            if (breedInfoId) {
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
        this._breedInfoService.createOrUpdateBreedInfo(this.breedInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.breedInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
