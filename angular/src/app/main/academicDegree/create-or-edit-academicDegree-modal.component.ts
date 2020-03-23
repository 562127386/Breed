import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { AcademicDegreeServiceProxy, AcademicDegreeCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditAcademicDegreeModal',
    templateUrl: './create-or-edit-academicDegree-modal.component.html'
})
export class CreateOrEditAcademicDegreeModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    academicDegree: AcademicDegreeCreateOrUpdateInput = new AcademicDegreeCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _academicDegreeService: AcademicDegreeServiceProxy
    ) {
        super(injector);
    }

    show(academicDegreeId?: number,editdisabled?: boolean): void {        
        if (!academicDegreeId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._academicDegreeService.getAcademicDegreeForEdit(academicDegreeId).subscribe(userResult => {
            this.academicDegree.name = userResult.name;
            this.academicDegree.code = userResult.code;
            this.academicDegree.id =  academicDegreeId;

            if (academicDegreeId) {
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
        this._academicDegreeService.createOrUpdateAcademicDegree(this.academicDegree)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.academicDegree);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
