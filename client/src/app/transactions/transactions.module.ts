import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransactionsComponent } from './transactions.component';
import { SharedModule } from '../shared/shared.module';
import { TransactionsService } from './transactions.service';
import { TransactionsRoutingModule } from './transactions-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { AddTransactionComponent } from './add-transaction/add-transaction.component';
import { FormsModule } from '@angular/forms';
import { SellStockComponent } from './sell-stock/sell-stock.component';



@NgModule({
  declarations: [TransactionsComponent, AddTransactionComponent, SellStockComponent],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    TransactionsRoutingModule
  ]
})
export class TransactionsModule { }
