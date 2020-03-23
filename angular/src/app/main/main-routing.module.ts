import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Dashboard2Component } from './dashboard2/dashboard2.component';
import { ContractorComponent } from './contractor/contractor.component';
import { OfficerComponent } from './officer/officer.component';
import { ProviderInfoComponent } from './providerInfo/providerInfo.component';
import { UnionInfoComponent } from './unionInfo/unionInfo.component';
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
import { EpidemiologicInfoComponent } from './epidemiologicInfo/epidemiologicInfo.component';
import { PlaqueStoreComponent } from './plaqueStore/plaqueStore.component';
import { PlaqueOfficerComponent } from './plaqueOfficer/plaqueOfficer.component';

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
                    { path: 'epidemiologicInfo', component: EpidemiologicInfoComponent },
                    { path: 'plaqueStore', component: PlaqueStoreComponent },
                    { path: 'plaqueOfficer', component: PlaqueOfficerComponent }

                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
