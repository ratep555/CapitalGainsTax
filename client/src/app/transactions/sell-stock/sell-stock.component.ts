import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { IStTransaction } from 'src/app/shared/models/transaction';
import { TransactionsService } from '../transactions.service';

@Component({
  selector: 'app-sell-stock',
  templateUrl: './sell-stock.component.html',
  styleUrls: ['./sell-stock.component.scss']
})
export class SellStockComponent implements OnInit {

  constructor(public transactionService: TransactionsService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    this.sellingStock(form);
}

sellingStock(form: NgForm) {
  this.transactionService.sellStock().subscribe(
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
