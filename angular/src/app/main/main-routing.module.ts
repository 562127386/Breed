import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContractorComponent } from './contractor/contractor.component';
import { ProviderInfoComponent } from './providerInfo/providerInfo.component';
import { SexInfoComponent } from './sexInfo/sexInfo.component';
import { SpeciesInfoComponent } from './speciesInfo/speciesInfo.component';
import { CityInfoComponent } from './cityInfo/cityInfo.component';
import { FirmTypeComponent } from './firmType/firmType.component';
import { PlaqueStateComponent } from './plaqueState/plaqueState.component';
import { StateInfoComponent } from './stateInfo/stateInfo.component';
import { VillageInfoComponent } from './villageInfo/villageInfo.component';
import { AcademicDegreeComponent } from './academicDegree/academicDegree.component';
import { FirmTypeServiceProxy } from '@shared/service-proxies/service-proxies';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: 'contractor', component: ContractorComponent },
                    { path: 'providerInfo', component: ProviderInfoComponent },
                    { path: 'sexInfo', component: SexInfoComponent },
                    { path: 'speciesInfo', component: SpeciesInfoComponent },
                    { path: 'cityInfo', component: CityInfoComponent },
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
