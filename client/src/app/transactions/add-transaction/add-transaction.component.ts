import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { IStockTransaction, IStTransaction } from 'src/app/shared/models/transaction';
import { StocksService } from 'src/app/stocks/stocks.service';
import { TransactionsService } from '../transactions.service';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.scss']
})
export class AddTransactionComponent implements OnInit {

  constructor(public transactionService: TransactionsService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
      this.creatingTransaction(form);
  }

  creatingTransaction(form: NgForm) {
    this.transactionService.createTransaction().subscribe(
     response => {
       this.resetForm(form);
       console.log('success!');
     }, error => {
       console.log(error);
     }
    );
  }

resetForm(form: NgForm) {
  form.form.reset();
  this.transactionService.formData = new IStTransaction();
}
}




