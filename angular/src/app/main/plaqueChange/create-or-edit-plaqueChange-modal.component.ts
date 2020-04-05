import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueChangeServiceProxy, PlaqueChangeCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { SpeciesInfoServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';
import * as momentjalali from 'jalali-moment';
import { format } from 'path';

@Component({
    selector: 'createOrEditPlaqueChangeModal',
    templateUrl: './create-or-edit-plaqueChange-modal.component.html'
})
export class CreateOrEditPlaqueChangeModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;    
    @ViewChild('plaqueStateCombobox', { static: true }) plaqueStateCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueChange: PlaqueChangeCreateOrUpdateInput = new PlaqueChangeCreateOrUpdateInput();    
    plaqueStatesSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    plaqueChangeOfficer: string;
    plaqueChangeNowDate: string;
    plaqueChangeNowTime: string;

    constructor(
        injector: Injector,
        private _plaqueChangeService: PlaqueChangeServiceProxy
    ) {
        super(injector);
    }

    show(plaqueChangeId?: number,editdisabled?: boolean): void {  
        if (!plaqueChangeId) {
            this.active = true;
            this.plaqueChangeOfficer = this.appSession.user.name + ' ' + this.appSession.user.surname;
            this.plaqueChangeNowDate = momentjalali().format('jYYYY/jMM/jDD');
            this.plaqueChangeNowTime = momentjalali().format('HH:mm');
        }
        else{
            this.plaqueChangeOfficer = '';
            this.plaqueChangeNowDate = '';
            this.plaqueChangeNowTime = '';
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueChangeService.getPlaqueChangeForEdit(plaqueChangeId).subscribe(userResult => {
            this.plaqueChange = userResult.plaqueChange;
            
            this.plaqueStatesSelectItems = _.map(userResult.plaqueStates, function(plaqueState) {
                return {
                    label: plaqueState.displayText, value: Number(plaqueState.value)
                };
            });

            
            if (plaqueChangeId) {
                this.active = true;
            }
            this.modal.show();
        });
        
    }

    onShown(): void {        
    }

    save(): void {
        let input = new PlaqueChangeCreateOrUpdateInput();
        input = this.plaqueChange;
                
        this.saving = true;
        this._plaqueChangeService.createOrUpdatePlaqueChange(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueChange);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    checkValidation(): void {                
        this._plaqueChangeService.checkValidation(this.plaqueChange).subscribe(userResult => {
            this.plaqueChange = userResult;
        });
    }
}