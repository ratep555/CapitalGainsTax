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
import { CollapseModule } from 'ngx-bootstrap/collapse';
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { DateInputComponent } from './components/date-input/date-input.component';

@NgModule({
  declarations: [PagerComponent, TextInputComponent, Pager1Component, MycurrencyPipe, DateInputComponent],
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    TypeaheadModule.forRoot(),
    ReactiveFormsModule,
    GoogleChartsModule.forRoot({ mapsApiKey: 'AIzaSyD-9tSrke72PouQMnMX-a7eZSW0jkFMBWY' }),
    CollapseModule.forRoot(),
    BsDatepickerModule.forRoot()

    ],
  exports: [
    PaginationModule,
    PagerComponent,
    Pager1Component,
    CarouselModule,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    DateInputComponent,
    MycurrencyPipe,
    GoogleChartsModule,
    BsDatepickerModule

  ]
})
export class SharedModule { }







