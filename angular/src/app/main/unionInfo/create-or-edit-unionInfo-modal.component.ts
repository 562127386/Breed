import { Component, ViewChild, Injector, ElementRef, Output, EventEmitter, ViewEncapsulation, AfterViewInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { UnionInfoServiceProxy, UnionInfoCreateOrUpdateInput, UnionEmployeeServiceProxy, UnionEmployeeListDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { SelectItem } from 'primeng/api';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    selector: 'createOrEditUnionInfoModal',
    templateUrl: './create-or-edit-unionInfo-modal.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class CreateOrEditUnionInfoModalComponent extends AppComponentBase implements AfterViewInit  {

    @ViewChild('createOrEditModal', {static: true}) modal: ModalDirective;
    @ViewChild('codeInput' , { static: false }) codeInput: ElementRef;
    @ViewChild('stateInfoCombobox', { static: true }) stateInfoCombobox: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    unionInfo: UnionInfoCreateOrUpdateInput = new UnionInfoCreateOrUpdateInput();   
    unionEmployees: UnionEmployeeListDto[] = [];
    stateInfosSelectItems: SelectItem[] = [];
    filterText : string = '';
    unionInfoId?: number;

    active: boolean = false;
    saving: boolean = false;
    editdisabled: boolean = false;
    nationalCodeValid: boolean = true;

    constructor(
        injector: Injector,
        private _unionInfoService: UnionInfoServiceProxy,
        private _unionEmployeeService: UnionEmployeeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector);
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }

    ngAfterViewInit(): void {
        //this.primengTableHelper.adjustScroll(this.dataTable);
    }

    show(unionInfoId?: number,editdisabled?: boolean): void {       
        this.unionInfoId = unionInfoId; 
        if (!unionInfoId) {
            this.active = true;
        }
        this.editdisabled = true;
        if (!editdisabled) {
            this.editdisabled = false;
        }
        this._unionInfoService.getUnionInfoForEdit(unionInfoId).subscribe(userResult => {            
            this.unionInfo = userResult.unionInfo;

            this.stateInfosSelectItems = _.map(userResult.stateInfos, function(stateInfo) {
                return {
                    label: stateInfo.displayText, value: Number(stateInfo.value)
                };
            });

            if (unionInfoId) {
                this.active = true;
            }

            this.modal.show();
        });
        
    }

    getUnionEmployee(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._unionEmployeeService.getUnionEmployee(
            this.filterText,
            this.unionInfoId,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator())).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createUnionEmployee(): void {
        //this.createOrEditAcademicDegreeModal.show();
    }

    deleteUnionEmployee(unionEmployee: UnionEmployeeListDto): void {
        this.message.confirm(
            this.l('AreYouSureToDeleteTheUnionEmployee', unionEmployee.name + ' ' + unionEmployee.family),            
            this.l('AreYouSure'),
            isConfirmed => {
                if (isConfirmed) {
                    this._unionEmployeeService.deleteUnionEmployee(unionEmployee.id).subscribe(() => {
                        this.reloadPage();
                        this.notify.info(this.l('SuccessfullyDeleted'));
                    });
                }
            }
        );
    } 

    onShown(): void {
        // this.codeInput.nativeElement.focus();
    }

    save(): void {
        
        if(this.nationalCodeValid){
            this.saving = true;
            this._unionInfoService.createOrUpdateUnionInfo(this.unionInfo)
                .pipe(finalize(() => this.saving = false))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(this.unionInfo);
                });
        }
    }

    close(): void {
        this.active = false;
        this.editdisabled = true;
        this.modal.hide();
    }

    checkNationalCode(): void {
        let meli_code = this.unionInfo.nationalCode;
        if (meli_code.length == 12) {
            if (meli_code == '111-111111-1' ||
                meli_code == '000-000000-0' ||
                meli_code == '222-222222-2' ||
                meli_code == '333-333333-3' ||
                meli_code == '444-444444-4' ||
                meli_code == '555-555555-5' ||
                meli_code == '666-666666-6' ||
                meli_code == '777-777777-7' ||
                meli_code == '888-888888-8' ||
                meli_code == '999-999999-9') {
                    this.nationalCodeValid = false;
            } else {
                let c = parseInt(meli_code.charAt(11));
                let n = parseInt(meli_code.charAt(0)) * 10 +
                    parseInt(meli_code.charAt(1)) * 9 +
                    parseInt(meli_code.charAt(2)) * 8 +
                    parseInt(meli_code.charAt(4)) * 7 +
                    parseInt(meli_code.charAt(5)) * 6 +
                    parseInt(meli_code.charAt(6)) * 5 +
                    parseInt(meli_code.charAt(7)) * 4 +
                    parseInt(meli_code.charAt(8)) * 3 +
                    parseInt(meli_code.charAt(9)) * 2;                  
                let r = n % 11;
                if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r)) {
                    this.nationalCodeValid = true;
                } else {
                    this.nationalCodeValid = false;
                }
            }
        } else {
            this.nationalCodeValid = false;
        }
    }
}
