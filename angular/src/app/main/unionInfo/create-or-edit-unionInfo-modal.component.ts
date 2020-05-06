import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { UnionInfoServiceProxy, UnionInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';

@Component({
    selector: 'createOrEditUnionInfoModal',
    templateUrl: './create-or-edit-unionInfo-modal.component.html'
})
export class CreateOrEditUnionInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    unionInfo: UnionInfoCreateOrUpdateInput = new UnionInfoCreateOrUpdateInput();   
    stateInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

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
        this.saving = true;
        this._unionInfoService.createOrUpdateUnionInfo(this.unionInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.unionInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
