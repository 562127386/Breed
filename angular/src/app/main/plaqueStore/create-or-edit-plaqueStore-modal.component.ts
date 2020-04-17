import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueStoreServiceProxy, PlaqueStoreCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { SpeciesInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';

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
    codePlaceHolder: string = '0';
    setTimeTemp: string;

    constructor(
        injector: Injector,
        private _plaqueStoreService: PlaqueStoreServiceProxy,
        private _speciesInfoService: SpeciesInfoServiceProxy
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
            
            this.setTimeTemp = this.getDate(userResult.plaqueStore.setTime);

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
        
        
        input.setTime = this.setDate(this.setTimeTemp);

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

    setCodeMask(speciesId : number): void {
        let speciesCode: string = '';
        this.codeMask = '0';
        this.codePlaceHolder = '0';
        this._speciesInfoService.getCodeRange(speciesId).subscribe(userResult => {
            this.codeMask = userResult;
            this.codePlaceHolder = userResult;            
        });
    }

    getDate(input: moment.Moment): string {
        if( input !== undefined){
            return input.format('YYYY/MM/DD');
        }
        return '';
    }

    setDate(input: string): moment.Moment {
        if( input !== undefined || input != ''){
            return moment(input);
        }
        return undefined;
    }
}
