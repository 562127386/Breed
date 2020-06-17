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
import { UnionEmployeeComponent } from "./unionEmployee/unionEmployee.component";
import { CreateOrEditUnionEmployeeModalComponent } from "./unionEmployee/create-or-edit-unionEmployee-modal.component";
import { ActivityInfoComponent } from "./activityInfo/activityInfo.component";
import { CreateOrEditActivityInfoModalComponent } from "./activityInfo/create-or-edit-activityInfo-modal.component";
import { SexInfoComponent } from "./sexInfo/sexInfo.component";
import { CreateOrEditSexInfoModalComponent } from "./sexInfo/create-or-edit-sexInfo-modal.component";
import { SpeciesInfoComponent } from "./speciesInfo/speciesInfo.component";
import { CreateOrEditSpeciesInfoModalComponent } from "./speciesInfo/create-or-edit-speciesInfo-modal.component";
import { AcademicDegreeComponent } from "./academicDegree/academicDegree.component";
import { CreateOrEditAcademicDegreeModalComponent } from "./academicDegree/create-or-edit-academicDegree-modal.component";
import { BreedInfoComponent } from "./breedInfo/breedInfo.component";
import { CreateOrEditBreedInfoModalComponent } from "./breedInfo/create-or-edit-breedInfo-modal.component";
import { BirthTypeInfoComponent } from "./birthTypeInfo/birthTypeInfo.component";
import { CreateOrEditBirthTypeInfoModalComponent } from "./birthTypeInfo/create-or-edit-birthTypeInfo-modal.component";
import { AnomalyInfoComponent } from "./anomalyInfo/anomalyInfo.component";
import { CreateOrEditAnomalyInfoModalComponent } from "./anomalyInfo/create-or-edit-anomalyInfo-modal.component";
import { MembershipInfoComponent } from "./membershipInfo/membershipInfo.component";
import { CreateOrEditMembershipInfoModalComponent } from "./membershipInfo/create-or-edit-membershipInfo-modal.component";
import { ManufacturerComponent } from "./manufacturer/manufacturer.component";
import { CreateOrEditManufacturerModalComponent } from "./manufacturer/create-or-edit-manufacturer-modal.component";
import { NoticeComponent } from "./notice/notice.component";
import { CreateOrEditNoticeModalComponent } from "./notice/create-or-edit-notice-modal.component";
import { EpidemiologicInfoComponent } from "./epidemiologicInfo/epidemiologicInfo.component";
import { CreateOrEditEpidemiologicInfoModalComponent } from "./epidemiologicInfo/create-or-edit-epidemiologicInfo-modal.component";
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
import { PlaqueToStateComponent } from "./plaqueToState/plaqueToState.component";
import { CreateOrEditPlaqueToStateModalComponent } from "./plaqueToState/create-or-edit-plaqueToState-modal.component";
import { PlaqueToContractorComponent } from "./plaqueToContractor/plaqueToContractor.component";
import { CreateOrEditPlaqueToContractorModalComponent } from "./plaqueToContractor/create-or-edit-plaqueToContractor-modal.component";
import { PlaqueToOfficerComponent } from "./plaqueToOfficer/plaqueToOfficer.component";
import { CreateOrEditPlaqueToOfficerModalComponent } from "./plaqueToOfficer/create-or-edit-plaqueToOfficer-modal.component";
import { PlaqueToHerdComponent } from "./plaqueToHerd/plaqueToHerd.component";
import { CreateOrEditPlaqueToHerdModalComponent } from "./plaqueToHerd/create-or-edit-plaqueToHerd-modal.component";
import { HerdComponent } from "./herd/herd.component";
import { CreateOrEditHerdModalComponent } from "./herd/create-or-edit-herd-modal.component";
import { PlaqueChangeComponent } from "./plaqueChange/plaqueChange.component";
import { CreateOrEditPlaqueChangeModalComponent } from "./plaqueChange/create-or-edit-plaqueChange-modal.component";
import { LivestockComponent } from "./livestock/livestock.component";
import { CreateOrEditLivestockModalComponent } from "./livestock/create-or-edit-livestock-modal.component";
import { HerdCertificateComponent } from "./herdCertificate/herdCertificate.component";
import { HerdCertificateReportComponent } from "./herdCertificate/herdCertificateReport.component";
import { HerdLivestockComponent } from "./herdLivestock/herdLivestock.component";
import { ReportHerdLivestockModalComponent } from './herdLivestock/report-herdLivestock-modal.component';
import { HerdGeoLogComponent } from "./herdGeoLog/herdGeoLog.component";
import { CreateOrEditHerdGeoLogModalComponent } from "./herdGeoLog/create-or-edit-herdGeoLog-modal.component";
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
        UnionEmployeeComponent,
        CreateOrEditUnionEmployeeModalComponent,
        ActivityInfoComponent,
        CreateOrEditActivityInfoModalComponent,
        SexInfoComponent,
        CreateOrEditSexInfoModalComponent,
        SpeciesInfoComponent,
        CreateOrEditSpeciesInfoModalComponent,
        AcademicDegreeComponent,
        CreateOrEditAcademicDegreeModalComponent,
        ManufacturerComponent,
        CreateOrEditManufacturerModalComponent,
        NoticeComponent,
        CreateOrEditNoticeModalComponent,
        EpidemiologicInfoComponent,
        CreateOrEditEpidemiologicInfoModalComponent,
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
        PlaqueToStateComponent,
        CreateOrEditPlaqueToStateModalComponent,
        PlaqueToContractorComponent,
        CreateOrEditPlaqueToContractorModalComponent,
        PlaqueToOfficerComponent,
        CreateOrEditPlaqueToOfficerModalComponent,
        PlaqueToHerdComponent,
        CreateOrEditPlaqueToHerdModalComponent,
        HerdComponent,
        CreateOrEditHerdModalComponent,
        LivestockComponent,
        CreateOrEditLivestockModalComponent,
        PlaqueChangeComponent,
        CreateOrEditPlaqueChangeModalComponent,
        HerdCertificateComponent,
        HerdLivestockComponent,
        ReportHerdLivestockModalComponent,
        HerdCertificateReportComponent,
        HerdGeoLogComponent,
        CreateOrEditHerdGeoLogModalComponent,
        BreedInfoComponent,
        CreateOrEditBreedInfoModalComponent,
        BirthTypeInfoComponent,
        CreateOrEditBirthTypeInfoModalComponent,
        AnomalyInfoComponent,
        CreateOrEditAnomalyInfoModalComponent,
        MembershipInfoComponent,
        CreateOrEditMembershipInfoModalComponent
    
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
