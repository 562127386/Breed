import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContractorComponent } from "./contractor/contractor.component";
import { CreateOrEditContractorModalComponent } from "./contractor/create-or-edit-contractor-modal.component";
import { ProviderInfoComponent } from "./providerInfo/providerInfo.component";
import { CreateOrEditProviderInfoModalComponent } from "./providerInfo/create-or-edit-providerInfo-modal.component";
import { SexInfoComponent } from "./sexInfo/sexInfo.component";
import { CreateOrEditSexInfoModalComponent } from "./sexInfo/create-or-edit-SexInfo-modal.component";
import { SpeciesInfoComponent } from "./speciesInfo/speciesInfo.component";
import { CreateOrEditSpeciesInfoModalComponent } from "./speciesInfo/create-or-edit-speciesInfo-modal.component";
import { AcademicDegreeComponent } from "./academicDegree/academicDegree.component";
import { CreateOrEditAcademicDegreeModalComponent } from "./academicDegree/create-or-edit-academicDegree-modal.component";
import { FirmTypeComponent } from "./firmType/firmType.component";
import { CreateOrEditFirmTypeModalComponent } from "./firmType/create-or-edit-firmType-modal.component";
import { PlaqueStateComponent } from "./plaqueState/plaqueState.component";
import { CreateOrEditPlaqueStateModalComponent } from "./plaqueState/create-or-edit-plaqueState-modal.component";
import { StateInfoComponent } from "./stateInfo/stateInfo.component";
import { CreateOrEditStateInfoModalComponent } from "./stateInfo/create-or-edit-stateInfo-modal.component";
import { VillageInfoComponent } from "./villageInfo/villageInfo.component";
import { CreateOrEditVillageInfoModalComponent} from "./villageInfo/create-or-edit-villageInfo-modal.component";
import { CityInfoComponent } from "./cityInfo/cityInfo.component";
import { CreateOrEditCityInfoModalComponent } from "./cityInfo/create-or-edit-cityInfo-modal.component";
import { AutoCompleteModule } from 'primeng/autocomplete';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule as PrimeNgFileUploadModule } from 'primeng/fileupload';
import { InputMaskModule } from 'primeng/inputmask';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { TreeModule } from 'primeng/tree';
import { DragDropModule } from 'primeng/dragdrop';
import { TreeDragDropService } from 'primeng/api';
import { ContextMenuModule } from 'primeng/contextmenu'
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(),
        TableModule,
        TreeModule,
        DragDropModule,
        ContextMenuModule,
        PaginatorModule,
        PrimeNgFileUploadModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule,
    ],
    declarations: [
        DashboardComponent,
        ContractorComponent,
        CreateOrEditContractorModalComponent,
        ProviderInfoComponent,
        CreateOrEditProviderInfoModalComponent,
        SexInfoComponent,
        CreateOrEditSexInfoModalComponent,
        SpeciesInfoComponent,
        CreateOrEditSpeciesInfoModalComponent,
        AcademicDegreeComponent,
        CreateOrEditAcademicDegreeModalComponent,
        FirmTypeComponent,
        CreateOrEditFirmTypeModalComponent,
        PlaqueStateComponent,
        CreateOrEditPlaqueStateModalComponent,
        StateInfoComponent,
        CreateOrEditStateInfoModalComponent,
        VillageInfoComponent,
        CreateOrEditVillageInfoModalComponent,
        CityInfoComponent,
        CreateOrEditCityInfoModalComponent
    
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
