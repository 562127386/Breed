import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ProviderInfoServiceProxy, ProviderInfoListDto, ListResultDtoOfProviderInfoListDto } from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';

@Component({
    templateUrl: './providerInfo.component.html',
    animations: [appModuleAnimation()]
})
export class ProviderInfoComponent extends AppComponentBase implements OnInit {

    providerInfos: ProviderInfoListDto[] = [];
    filter: string = '';

    constructor(
        injector: Injector,
        private _providerInfoService: ProviderInfoServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.getProviderInfo();
    }

    getProviderInfo(): void {
        this._providerInfoService.getProviderInfo(this.filter).subscribe((result) => {
            this.providerInfos = result.items;
        });
    }

    deleteProviderInfo(providerInfo: ProviderInfoListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheProviderInfo', providerInfo.name),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._providerInfoService.deleteProviderInfo(providerInfo.id).subscribe(() => {
                        this.notify.info(this.l('SuccessfullyDeleted'));
                        _.remove(this.providerInfos, providerInfo);
                    });
                }
            }
        );
    } 
    
}
