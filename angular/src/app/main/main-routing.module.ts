import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Dashboard2Component } from './dashboard2/dashboard2.component';
import { ContractorComponent } from './contractor/contractor.component';
import { OfficerComponent } from './officer/officer.component';
import { ProviderInfoComponent } from './providerInfo/providerInfo.component';
import { UnionInfoComponent } from './unionInfo/unionInfo.component';
import { UnionEmployeeComponent } from './unionEmployee/unionEmployee.component';
import { ActivityInfoComponent } from './activityInfo/activityInfo.component';
import { SexInfoComponent } from './sexInfo/sexInfo.component';
import { SpeciesInfoComponent } from './speciesInfo/speciesInfo.component';
import { CityInfoComponent } from './cityInfo/cityInfo.component';
import { RegionInfoComponent } from './regionInfo/regionInfo.component';
import { FirmTypeComponent } from './firmType/firmType.component';
import { PlaqueStateComponent } from './plaqueState/plaqueState.component';
import { StateInfoComponent } from './stateInfo/stateInfo.component';
import { VillageInfoComponent } from './villageInfo/villageInfo.component';
import { AcademicDegreeComponent } from './academicDegree/academicDegree.component';
import { ManufacturerComponent } from './manufacturer/manufacturer.component';
import { NoticeComponent } from './notice/notice.component';
import { EpidemiologicInfoComponent } from './epidemiologicInfo/epidemiologicInfo.component';
import { PlaqueStoreComponent } from './plaqueStore/plaqueStore.component';
import { PlaqueToStateComponent } from './plaqueToState/plaqueToState.component';
import { PlaqueToContractorComponent } from './plaqueToContractor/plaqueToContractor.component';
import { PlaqueToOfficerComponent } from './plaqueToOfficer/plaqueToOfficer.component';
import { PlaqueToHerdComponent } from './plaqueToHerd/plaqueToHerd.component';
import { HerdComponent } from './herd/herd.component';
import { LivestockComponent } from './livestock/livestock.component';
import { PlaqueChangeComponent } from './plaqueChange/plaqueChange.component';
import { HerdCertificateComponent } from './herdCertificate/herdCertificate.component';
import { HerdLivestockComponent } from "./herdLivestock/herdLivestock.component";
import { HerdGeoLogComponent } from './herdGeoLog/herdGeoLog.component';
import { BreedInfoComponent } from './breedInfo/breedInfo.component';
import { BirthTypeInfoComponent } from './birthTypeInfo/birthTypeInfo.component';
import { AnomalyInfoComponent } from './anomalyInfo/anomalyInfo.component';
import { MembershipInfoComponent } from './membershipInfo/membershipInfo.component';
import { BodyColorInfoComponent } from './bodyColorInfo/bodyColorInfo.component';
import { SpotConnectorInfoComponent } from './spotConnectorInfo/spotConnectorInfo.component';
import { InseminationComponent } from './insemination/insemination.component';
@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: '', redirectTo: 'dashboard2', pathMatch: 'full' },
                    { path: 'dashboard2', component: Dashboard2Component },
                    { path: 'contractor', component: ContractorComponent },
                    { path: 'officer', component: OfficerComponent },
                    { path: 'providerInfo', component: ProviderInfoComponent },
                    { path: 'unionInfo', component: UnionInfoComponent },                    
                    { path: 'unionInfo/:id/employees', component: UnionEmployeeComponent, data: { permission: 'Pages.BaseIntro.UnionEmployee' } },
                    { path: 'activityInfo', component: ActivityInfoComponent },
                    { path: 'sexInfo', component: SexInfoComponent },
                    { path: 'speciesInfo', component: SpeciesInfoComponent },
                    { path: 'cityInfo', component: CityInfoComponent },
                    { path: 'regionInfo', component: RegionInfoComponent },
                    { path: 'firmType', component: FirmTypeComponent },
                    { path: 'plaqueState', component: PlaqueStateComponent },
                    { path: 'stateInfo', component: StateInfoComponent },
                    { path: 'villageInfo', component: VillageInfoComponent },
                    { path: 'academicDegree', component: AcademicDegreeComponent },
                    { path: 'manufacturer', component: ManufacturerComponent },
                    { path: 'notice', component: NoticeComponent },
                    { path: 'epidemiologicInfo', component: EpidemiologicInfoComponent },
                    { path: 'plaqueStore', component: PlaqueStoreComponent },
                    { path: 'plaqueToState', component: PlaqueToStateComponent },
                    { path: 'plaqueToContractor', component: PlaqueToContractorComponent },
                    { path: 'plaqueToOfficer', component: PlaqueToOfficerComponent },
                    { path: 'plaqueToHerd', component: PlaqueToHerdComponent },
                    { path: 'herd', component: HerdComponent },
                    { path: 'livestock', component: LivestockComponent },
                    { path: 'plaqueChange', component: PlaqueChangeComponent },
                    { path: 'herdCertificate', component: HerdCertificateComponent },
                    { path: 'herdLivestock', component: HerdLivestockComponent },
                    { path: 'herdGeoLog', component: HerdGeoLogComponent },
                    { path: 'breedInfo', component: BreedInfoComponent },
                    { path: 'birthTypeInfo', component: BirthTypeInfoComponent },
                    { path: 'anomalyInfo', component: AnomalyInfoComponent },
                    { path: 'membershipInfo', component: MembershipInfoComponent },
                    { path: 'bodyColorInfo', component: BodyColorInfoComponent },
                    { path: 'spotConnectorInfo', component: SpotConnectorInfoComponent },
                    { path: 'insemination', component: InseminationComponent }

                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
