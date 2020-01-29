import { Component, Injector, ViewEncapsulation, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ContractorServiceProxy, ContractorListDto, ListResultDtoOfContractorListDto } from '@shared/service-proxies/service-proxies';

@Component({
    templateUrl: './contractor.component.html',
    styleUrls: ['./contractor.component.less'],
    animations: [appModuleAnimation()],
    encapsulation: ViewEncapsulation.None
})

export class ContractorComponent extends AppComponentBase implements OnInit {
   
    contractors: ContractorListDto[] = [];
    filter: string = '';

    constructor(
        injector: Injector,
        private _contractorService: ContractorServiceProxy) {
        super(injector);
    }

    ngOnInit(): void {
        this.getContractors();
    }

    getContractors(): void {
        this._contractorService.getContractors().subscribe((result) => {
            this.contractors = result.items;
        });
    }
}