import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { Dashboard2Component } from './dashboard2/dashboard2.component';
import { ContractorComponent } from './contractor/contractor.component';
import { OfficerComponent } from './officer/officer.component';
import { ProviderInfoComponent } from './providerInfo/providerInfo.component';
import { SexInfoComponent } from './sexInfo/sexInfo.component';
import { SpeciesInfoComponent } from './speciesInfo/speciesInfo.component';
import { CityInfoComponent } from './cityInfo/cityInfo.component';
import { RegionInfoComponent } from './regionInfo/regionInfo.component';
import { FirmTypeComponent } from './firmType/firmType.component';
import { PlaqueStateComponent } from './plaqueState/plaqueState.component';
import { StateInfoComponent } from './stateInfo/stateInfo.component';
import { VillageInfoComponent } from './villageInfo/villageInfo.component';
import { AcademicDegreeComponent } from './academicDegree/academicDegree.component';

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
                    { path: 'sexInfo', component: SexInfoComponent },
                    { path: 'speciesInfo', component: SpeciesInfoComponent },
                    { path: 'cityInfo', component: CityInfoComponent },
                    { path: 'regionInfo', component: RegionInfoComponent },
                    { path: 'firmType', component: FirmTypeComponent },
                    { path: 'plaqueState', component: PlaqueStateComponent },
                    { path: 'stateInfo', component: StateInfoComponent },
                    { path: 'villageInfo', component: VillageInfoComponent },
                    { path: 'academicDegree', component: AcademicDegreeComponent }

                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
