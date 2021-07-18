import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ChartsComponent } from './charts.component';
import { ChartsRoutingModule } from './charts-routing.module';


@NgModule({
  declarations: [ChartsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ChartsRoutingModule
  ]
})
export class ChartsModule { }
