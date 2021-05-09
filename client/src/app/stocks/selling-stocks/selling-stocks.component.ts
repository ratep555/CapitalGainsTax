import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { IStTransaction } from 'src/app/shared/models/transaction';
import { TransactionsService } from 'src/app/transactions/transactions.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-selling-stocks',
  templateUrl: './selling-stocks.component.html',
  styleUrls: ['./selling-stocks.component.scss']
})
export class SellingStocksComponent implements OnInit {
  stock: IStock;

  constructor(public stocksService: StocksService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private transactionService: TransactionsService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadStock();
  }

  loadStock() {
    return this.stocksService.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.stock = response;
      this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });
  }

  onSubmit(form: NgForm) {
    this.stocksService.formData.stockId = this.stock.id;
    this.sellingStock(form);
}


  sellingStock(form: NgForm) {
    this.stocksService.sellStock().subscribe(
      response => {
      //  this.stock.id = this.stocksService.formData.stockId;
        this.resetForm(form);
        this.router.navigateByUrl('myportfolio');
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
