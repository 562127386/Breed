import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { ProviderInfoServiceProxy, ProviderInfoCreateInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'createProviderInfoModal',
    templateUrl: './create-providerInfo-modal.component.html'
})
export class CreateProviderInfoModalComponent extends AppComponentBase {

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @ViewChild('modal' , { static: false }) modal: ModalDirective;
    @ViewChild('nameInput' , { static: false }) nameInput: ElementRef;

    providerInfo: ProviderInfoCreateInput = new ProviderInfoCreateInput();

    active: boolean = false;
    saving: boolean = false;

    constructor(
        injector: Injector,
        private _providerInfoService: ProviderInfoServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.providerInfo = new ProviderInfoCreateInput();
        this.modal.show();
    }

    onShown(): void {
        this.nameInput.nativeElement.focus();
    }

    save(): void {
        this.saving = true;
        this._providerInfoService.createProviderInfo(this.providerInfo)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(this.providerInfo);
            });
    }

    close(): void {
        this.modal.hide();
        this.active = false;
    }
}
