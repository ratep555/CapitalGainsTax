import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyportfolioComponent } from './myportfolio.component';
import { SharedModule } from '../shared/shared.module';
import { MyportfolioRoutingModule } from './myportfolio-routing.module';
import { StockDetailComponent } from './stock-detail/stock-detail.component';
import { FormsModule } from '@angular/forms';
import { SellStockComponent } from './sell-stock/sell-stock.component';



@NgModule({
  declarations: [MyportfolioComponent, StockDetailComponent, SellStockComponent],
  imports: [
    CommonModule,
    SharedModule,
    MyportfolioRoutingModule,
    FormsModule
  ]
})
export class MyportfolioModule { }
