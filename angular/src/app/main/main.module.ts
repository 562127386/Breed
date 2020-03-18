import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Dashboard2Component } from './dashboard2/dashboard2.component';
import { ContractorComponent } from "./contractor/contractor.component";
import { CreateOrEditContractorModalComponent } from "./contractor/create-or-edit-contractor-modal.component";
import { OfficerComponent } from "./officer/officer.component";
import { CreateOrEditOfficerModalComponent } from "./officer/create-or-edit-officer-modal.component";
import { ProviderInfoComponent } from "./providerInfo/providerInfo.component";
import { CreateOrEditProviderInfoModalComponent } from "./providerInfo/create-or-edit-providerInfo-modal.component";
import { UnionInfoComponent } from "./unionInfo/unionInfo.component";
import { CreateOrEditUnionInfoModalComponent } from "./unionInfo/create-or-edit-unionInfo-modal.component";
import { ActivityInfoComponent } from "./activityInfo/activityInfo.component";
import { CreateOrEditActivityInfoModalComponent } from "./activityInfo/create-or-edit-activityInfo-modal.component";
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
import { RegionInfoComponent } from "./regionInfo/regionInfo.component";
import { CreateOrEditRegionInfoModalComponent } from "./regionInfo/create-or-edit-regionInfo-modal.component";
import { PlaqueStoreComponent } from "./plaqueStore/plaqueStore.component";
import { CreateOrEditPlaqueStoreModalComponent } from "./plaqueStore/create-or-edit-plaqueStore-modal.component";
import { PlaqueOfficerComponent } from "./plaqueOfficer/plaqueOfficer.component";
import { CreateOrEditPlaqueOfficerModalComponent } from "./plaqueOfficer/create-or-edit-plaqueOfficer-modal.component";
import { AutoCompleteModule } from 'primeng/autocomplete';
import { EditorModule } from 'primeng/editor';
import { FileUploadModule as PrimeNgFileUploadModule } from 'primeng/fileupload';
import { InputMaskModule } from 'primeng/inputmask';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { TreeModule } from 'primeng/tree';
import { DragDropModule } from 'primeng/dragdrop';
import { FieldsetModule } from 'primeng/fieldset';
import { KeyFilterModule } from 'primeng/keyfilter';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TreeDragDropService } from 'primeng/api';
import { ContextMenuModule } from 'primeng/contextmenu'
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DropdownModule } from 'primeng/dropdown'

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
        FieldsetModule,
        KeyFilterModule,
        InputTextareaModule,
        ContextMenuModule,
        PaginatorModule,
        PrimeNgFileUploadModule,
        AutoCompleteModule,
        EditorModule,
        InputMaskModule,
        DropdownModule
    ],
    declarations: [
        DashboardComponent,
        Dashboard2Component,
        ContractorComponent,
        CreateOrEditContractorModalComponent,
        OfficerComponent,
        CreateOrEditOfficerModalComponent,
        ProviderInfoComponent,
        CreateOrEditProviderInfoModalComponent,
        UnionInfoComponent,
        CreateOrEditUnionInfoModalComponent,
        ActivityInfoComponent,
        CreateOrEditActivityInfoModalComponent,
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
        CreateOrEditCityInfoModalComponent,
        RegionInfoComponent,
        CreateOrEditRegionInfoModalComponent,
        PlaqueStoreComponent,
        CreateOrEditPlaqueStoreModalComponent,
        PlaqueOfficerComponent,
        CreateOrEditPlaqueOfficerModalComponent
    
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
