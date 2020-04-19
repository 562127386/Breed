import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { PlaqueOfficerServiceProxy, PlaqueOfficerCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditPlaqueOfficerModal',
    templateUrl: './create-or-edit-plaqueOfficer-modal.component.html'
})
export class CreateOrEditPlaqueOfficerModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;    
    @ViewChild('speciesInfoCombobox', { static: true }) speciesInfoCombobox: ElementRef;
    @ViewChild('officerCombobox', { static: true }) officerCombobox: ElementRef;
    
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    plaqueOfficer: PlaqueOfficerCreateOrUpdateInput = new PlaqueOfficerCreateOrUpdateInput();    
    speciesInfosSelectItems: SelectItem[] = [];
    officersSelectItems: SelectItem[] = [];

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    setTimeTemp: string;

    constructor(
        injector: Injector,
        private _plaqueOfficerService: PlaqueOfficerServiceProxy
    ) {
        super(injector);
    }

    show(plaqueOfficerId?: number,editdisabled?: boolean): void {  
        if (!plaqueOfficerId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._plaqueOfficerService.getPlaqueOfficerForEdit(plaqueOfficerId).subscribe(userResult => {
            this.plaqueOfficer = userResult.plaqueOfficer;
            
            this.setTimeTemp = this.getDate(userResult.plaqueOfficer.setTime);

            this.speciesInfosSelectItems = _.map(userResult.speciesInfos, function(speciesInfo) {
                return {
                    label: speciesInfo.displayText, value: Number(speciesInfo.value)
                };
            });

            this.officersSelectItems = _.map(userResult.officers, function(officer) {
                return {
                    label: officer.displayText, value: Number(officer.value)
                };
            });

            if (plaqueOfficerId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        let input = new PlaqueOfficerCreateOrUpdateInput();
        input = this.plaqueOfficer;
        this.saving = true;        
        
        input.setTime = this.setDate(this.setTimeTemp);
        
        this._plaqueOfficerService.createOrUpdatePlaqueOfficer(input)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.plaqueOfficer);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
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
