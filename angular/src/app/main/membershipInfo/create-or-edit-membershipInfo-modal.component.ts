import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { MembershipInfoServiceProxy, MembershipInfoCreateOrUpdateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createOrEditMembershipInfoModal',
    templateUrl: './create-or-edit-membershipInfo-modal.component.html'
})
export class CreateOrEditMembershipInfoModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    membershipInfo: MembershipInfoCreateOrUpdateInput = new MembershipInfoCreateOrUpdateInput();

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;

    constructor(
        injector: Injector,
        private _membershipInfoService: MembershipInfoServiceProxy
    ) {
        super(injector);
    }

    show(membershipInfoId?: number,editdisabled?: boolean): void {        
        if (!membershipInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._membershipInfoService.getMembershipInfoForEdit(membershipInfoId).subscribe(userResult => {
            this.membershipInfo.name = userResult.name;
            this.membershipInfo.code = userResult.code;
            this.membershipInfo.id =  membershipInfoId;

            if (membershipInfoId) {
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
        this._membershipInfoService.createOrUpdateMembershipInfo(this.membershipInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.membershipInfo);
            });
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

}
