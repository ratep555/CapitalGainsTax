import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { TransactionsComponent } from './transactions.component';
import { AddTransactionComponent } from './add-transaction/add-transaction.component';
import { SellStockComponent } from './sell-stock/sell-stock.component';

const routes: Routes = [
  {path: '', component: TransactionsComponent},
  {path: 'addtransaction', component: AddTransactionComponent},
  {path: 'sellstock', component: SellStockComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]

})
export class TransactionsRoutingModule { }
