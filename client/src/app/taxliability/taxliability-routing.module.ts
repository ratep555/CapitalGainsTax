import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { TaxliabilityComponent } from './taxliability.component';
import { TaxLiabilityDetailComponent } from './tax-liability-detail/tax-liability-detail.component';
import { TaxLiabilityDetail1Component } from './tax-liability-detail1/tax-liability-detail1.component';


const routes: Routes = [
 {path: '', component: TaxliabilityComponent},
{path: 'tax', component: TaxLiabilityDetailComponent, data: {breadcrumb: 'Tax Details'}},
{path: 'taxy', component: TaxLiabilityDetail1Component, data: {breadcrumb: 'Tax Liability'}}


];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]

})
export class TaxliabilityRoutingModule { }
