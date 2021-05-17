import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MyportfolioComponent } from './myportfolio.component';
import { SellStockComponent } from '../transactions/sell-stock/sell-stock.component';
import { StockDetailComponent } from './stock-detail/stock-detail.component';

const routes: Routes = [
  {path: '', component: MyportfolioComponent},
//  {path: ':id', component: StockDetailComponent, data: {breadcrumb: 'Stock Detail'}},
  {path: 'sellstock/:id', component: SellStockComponent, data: {breadcrumb: 'Sell Stock'}}

];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]

})
export class MyportfolioRoutingModule { }
