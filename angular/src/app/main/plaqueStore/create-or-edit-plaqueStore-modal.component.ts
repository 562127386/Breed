import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueStoreServiceProxy, PlaqueStoreCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';

@Component({
    selector: 'createOrEditPlaqueStoreModal',
    templateUrl: './create-or-edit-plaqueStore-modal.component.html'
})
export class CreateOrEditPlaqueStoreModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueStore: PlaqueStoreCreateOrUpdateInput = new PlaqueStoreCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    codeMask: string = '0';
    codePlaceHolder: string = 'X';

    constructor(
        injector: Injector,
        private _plaqueStoreService: PlaqueStoreServiceProxy
    ) {
        super(injector);
    }

    show(plaqueStoreId?: number,editdisabled?: boolean): void {  
        if (!plaqueStoreId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueStoreService.getPlaqueStoreForEdit(plaqueStoreId).subscribe(userResult => {
            this.plaqueStore = userResult.plaqueStore;
            
            this.speciesInfosSelectItems = _.map(userResult.specieInfos, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            if (plaqueStoreId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        let input = new PlaqueStoreCreateOrUpdateInput();
        input = this.plaqueStore;
        this.saving = true;
        this._plaqueStoreService.createOrUpdatePlaqueStore(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueStore);
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

    setCodeMask(speciesCode : number): void {
        if(speciesCode < 10){
            this.codeMask = '999-9-99-9-99999999';
            this.codePlaceHolder = '364-0-52-' + speciesCode + '-99999999';
        }
        else if(speciesCode < 100){
            var speciesCodetxt = speciesCode.toString();
            this.codeMask = '999-9-99-9-99999999';
            this.codePlaceHolder = '364-0-52-' + speciesCodetxt[0]+ '-' + speciesCodetxt[0] + '9999999';
        }
    }
}
