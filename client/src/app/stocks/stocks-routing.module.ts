import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StocksComponent } from './stocks.component';
import { StockDetailsComponent } from './stock-details/stock-details.component';
import { AddStockComponent } from './add-stock/add-stock.component';

const routes: Routes = [
  {path: '', component: StocksComponent},
  {path: ':id', component: StockDetailsComponent, data: {breadcrumb: {alias: 'stockDetails'}}},
  {path: 'addstock', component: AddStockComponent}
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]


})
export class StocksRoutingModule { }
