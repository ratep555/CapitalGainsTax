import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StocksAdminAddComponent } from './stocks-admin-add/stocks-admin-add.component';
import { StocksAdminComponent } from './stocks-admin.component';
import { StocksAdminAdd1Component } from './stocks-admin-add1/stocks-admin-add1.component';
import { StocksAdminEditComponent } from './stocks-admin-edit/stocks-admin-edit.component';

const routes: Routes = [
  {path: '', component: StocksAdminComponent},
  {path: 'addadminstock', component: StocksAdminAddComponent, data: {breadcrumb: 'Add Stock'}},
  {path: 'addadminstock1', component: StocksAdminAdd1Component, data: {breadcrumb: 'Add Stock'}},
  {path: 'editstock/:id', component: StocksAdminEditComponent, data: {breadcrumb: 'Update Stock'}}

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class StocksAdminRoutingModule { }
