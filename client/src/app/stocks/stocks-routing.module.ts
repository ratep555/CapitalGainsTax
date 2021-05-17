import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { StocksComponent } from './stocks.component';
import { StockDetailsComponent } from './stock-details/stock-details.component';
import { AddStockComponent } from './add-stock/add-stock.component';
import { SellingStocksComponent } from './selling-stocks/selling-stocks.component';
import { AddStockReactiveFormComponent } from './add-stock-reactive-form/add-stock-reactive-form.component';
import { SellStockReactiveFormComponent } from './sell-stock-reactive-form/sell-stock-reactive-form.component';

const routes: Routes = [
  {path: '', component: StocksComponent},
  {path: ':id', component: StockDetailsComponent, data: {breadcrumb: {alias: 'stockDetails'}}},
  {path: 'addstock/:id', component: AddStockComponent, data: {breadcrumb: 'Add Stock'}},
  {path: 'sellingstocks/:id', component: SellingStocksComponent, data: {breadcrumb: 'Sell Stock'}},
  {path: 'addstockReactive/:id', component: AddStockReactiveFormComponent, data: {breadcrumb: 'Buy Stock'}},
  {path: 'sellstockReactive/:id', component: SellStockReactiveFormComponent, data: {breadcrumb: 'Sell Stock'}}

];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]


})
export class StocksRoutingModule { }
