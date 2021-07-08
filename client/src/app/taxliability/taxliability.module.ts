import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxliabilityComponent } from './taxliability.component';
import { TaxLiabilityDetailComponent } from './tax-liability-detail/tax-liability-detail.component';
import { TaxliabilityRoutingModule } from './taxliability-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { TaxLiabilityDetail1Component } from './tax-liability-detail1/tax-liability-detail1.component';




@NgModule({
  declarations: [TaxliabilityComponent, TaxLiabilityDetailComponent, TaxLiabilityDetail1Component],
  imports: [
    CommonModule,
    TaxliabilityRoutingModule,
    SharedModule,
    FormsModule,
    TypeaheadModule.forRoot()
  ],
})
export class TaxliabilityModule { }
