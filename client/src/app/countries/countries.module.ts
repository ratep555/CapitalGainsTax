import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountriesComponent } from './countries.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CountriesRoutingModule } from './countries-routing.module';
import { AddCountryComponent } from './add-country/add-country.component';
import { EditCountryComponent } from './edit-country/edit-country.component';



@NgModule({
  declarations: [CountriesComponent, AddCountryComponent, EditCountryComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    CountriesRoutingModule
  ]
})
export class CountriesModule { }
