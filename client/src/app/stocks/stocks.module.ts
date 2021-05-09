import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StocksComponent } from './stocks.component';
import { SharedModule } from '../shared/shared.module';
import { StockDetailsComponent } from './stock-details/stock-details.component';
import { StocksRoutingModule } from './stocks-routing.module';
import { AddStockComponent } from './add-stock/add-stock.component';
import { FormsModule } from '@angular/forms';
import { SellingStocksComponent } from './selling-stocks/selling-stocks.component';
import { AddStockReactiveFormComponent } from './add-stock-reactive-form/add-stock-reactive-form.component';
import { SellStockReactiveFormComponent } from './sell-stock-reactive-form/sell-stock-reactive-form.component';



@NgModule({
  declarations: [StocksComponent, StockDetailsComponent, AddStockComponent, SellingStocksComponent, AddStockReactiveFormComponent, SellStockReactiveFormComponent],
  imports: [
    CommonModule,
    SharedModule,
    StocksRoutingModule,
    FormsModule
  ]

})
export class StocksModule { }
