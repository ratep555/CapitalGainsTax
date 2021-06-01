import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CountriesComponent } from './countries.component';
import { AddCountryComponent } from './add-country/add-country.component';
import { EditCountryComponent } from './edit-country/edit-country.component';

const routes: Routes = [
  {path: '', component: CountriesComponent},
  {path: 'addcountry', component: AddCountryComponent, data: {breadcrumb: 'Add Country'}},
  {path: 'editcountry/:id', component: EditCountryComponent, data: {breadcrumb: 'Update Country'}}

];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CountriesRoutingModule { }
