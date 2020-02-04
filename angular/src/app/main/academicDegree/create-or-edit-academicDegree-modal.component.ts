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
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    academicDegree: AcademicDegreeCreateOrUpdateInput = new AcademicDegreeCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _academicDegreeService: AcademicDegreeServiceProxy
    ) {
        super(injector);
    }

    show(academicDegreeId?: number): void {
        if (!academicDegreeId) {
            this.active = true;
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
        this.nameInput.nativeElement.focus();
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
        this.modal.hide();
    }
}
