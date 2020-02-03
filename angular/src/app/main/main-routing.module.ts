import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ContractorComponent } from './contractor/contractor.component';
import { ProviderInfoComponent } from './providerInfo/providerInfo.component';
import { SexInfoComponent } from './sexInfo/sexInfo.component';
import { SpeciesInfoComponent } from './speciesInfo/speciesInfo.component';

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
                    { path: 'speciesInfo', component: SpeciesInfoComponent }

                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
