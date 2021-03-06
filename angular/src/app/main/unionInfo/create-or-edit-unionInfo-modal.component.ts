import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { UnionInfoServiceProxy, UnionInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';

@Component({
    selector: 'createOrEditUnionInfoModal',
    templateUrl: './create-or-edit-unionInfo-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class CreateOrEditUnionInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: true }) codeInput: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    unionInfo: UnionInfoCreateOrUpdateInput = new UnionInfoCreateOrUpdateInput();   
    stateInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    nationalCodeValid: boolean = true;

    constructor(
        injector: Injector,
        private _unionInfoService: UnionInfoServiceProxy
    ) {
        super(injector);
    }
  
    show(unionInfoId?: number,editdisabled?: boolean): void {  
        if (!unionInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._unionInfoService.getUnionInfoForEdit(unionInfoId).subscribe(userResult => {            
            this.unionInfo = userResult.unionInfo;

            this.stateInfosSelectItems = _.map(userResult.stateInfos, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            if (unionInfoId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        
        if(this.nationalCodeValid){
            this.saving = true;
            this._unionInfoService.createOrUpdateUnionInfo(this.unionInfo)
                .pipe(finalize(() => this.saving = false))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(this.unionInfo);
                });
        }
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    checkNationalCode(): void {
        let meli_code = this.unionInfo.nationalCode;
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
