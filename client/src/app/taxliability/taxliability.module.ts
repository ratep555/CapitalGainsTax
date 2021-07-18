import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxliabilityComponent } from './taxliability.component';
import { TaxLiabilityDetailComponent } from './tax-liability-detail/tax-liability-detail.component';
import { TaxliabilityRoutingModule } from './taxliability-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { TaxLiabilityDetail1Component } from './tax-liability-detail1/tax-liability-detail1.component';
import { TaxLiabilityDetail2Component } from './tax-liability-detail2/tax-liability-detail2.component';
import { ChartsModule } from '../charts/charts.module';
import { ChartsComponent } from '../charts/charts.component';
import { ChartsRoutingModule } from '../charts/charts-routing.module';




@NgModule({
  declarations: [
    TaxliabilityComponent,
    TaxLiabilityDetailComponent,
    TaxLiabilityDetail1Component,
    TaxLiabilityDetail2Component],
  imports: [
    CommonModule,
    TaxliabilityRoutingModule,
    SharedModule,
    FormsModule,
    TypeaheadModule.forRoot()
  ],
})
export class TaxliabilityModule { }
