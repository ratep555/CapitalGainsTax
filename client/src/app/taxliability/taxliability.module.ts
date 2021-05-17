import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxliabilityComponent } from './taxliability.component';
import { TaxLiabilityDetailComponent } from './tax-liability-detail/tax-liability-detail.component';
import { TaxliabilityRoutingModule } from './taxliability-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';




@NgModule({
  declarations: [TaxliabilityComponent, TaxLiabilityDetailComponent],
  imports: [
    CommonModule,
    TaxliabilityRoutingModule,
    SharedModule,
    FormsModule
  ],
})
export class TaxliabilityModule { }
