import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StocksAdminComponent } from './stocks-admin.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { StocksAdminRoutingModule } from './stocks-admin-routing.module';
import { StocksAdminAddComponent } from './stocks-admin-add/stocks-admin-add.component';
import { StocksAdminEditComponent } from './stocks-admin-edit/stocks-admin-edit.component';
import { StocksAdminAdd1Component } from './stocks-admin-add1/stocks-admin-add1.component';



@NgModule({
  declarations: [StocksAdminComponent, StocksAdminAddComponent, StocksAdminEditComponent, StocksAdminAdd1Component],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    StocksAdminRoutingModule
  ]
})
export class StocksAdminModule { }
