import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { IStockTransaction, IStTransaction } from 'src/app/shared/models/transaction';
import { StocksService } from 'src/app/stocks/stocks.service';
import { TransactionsService } from '../transactions.service';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.scss']
})
export class AddTransactionComponent implements OnInit {

  constructor(public transactionService: TransactionsService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
      this.creatingTransaction(form);
    /*   this.creatingTransaction(form).subscribe(response => {
        this.router.navigateByUrl('transactions');
      }, error => {
        console.log(error);
      }
      ); */
  }

  creatingTransaction(form: NgForm) {
    this.transactionService.createTransaction().subscribe(
     response => {
       this.resetForm(form);
       this.router.navigateByUrl('transactions');
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




