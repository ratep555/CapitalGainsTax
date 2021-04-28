import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StocksComponent } from './stocks.component';
import { SharedModule } from '../shared/shared.module';
import { StockDetailsComponent } from './stock-details/stock-details.component';
import { StocksRoutingModule } from './stocks-routing.module';
import { AddStockComponent } from './add-stock/add-stock.component';



@NgModule({
  declarations: [StocksComponent, StockDetailsComponent, AddStockComponent],
  imports: [
    CommonModule,
    SharedModule,
    StocksRoutingModule
  ]

})
export class StocksModule { }
