import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagerComponent } from './components/pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { Pager1Component } from './components/pager1/pager1.component';
import { MycurrencyPipe } from './pipes/custom.currencypipe';
import { TypeaheadModule } from 'ngx-bootstrap/typeahead';
import { GoogleChartsModule } from 'angular-google-charts';
import { ChartsModule } from '../charts/charts.module';
import { ChartsComponent } from '../charts/charts.component';




@NgModule({
  declarations: [PagerComponent, TextInputComponent, Pager1Component, MycurrencyPipe],
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    TypeaheadModule.forRoot(),
    ReactiveFormsModule,
    GoogleChartsModule.forRoot({ mapsApiKey: 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY' })

    ],
  exports: [
    PaginationModule,
    PagerComponent,
    Pager1Component,
    CarouselModule,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    MycurrencyPipe,
    GoogleChartsModule
  ]
})
export class SharedModule { }







