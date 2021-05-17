import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { StocksService } from 'src/app/stocks/stocks.service';
import { TransactionsService } from 'src/app/transactions/transactions.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { MyportfolioService } from '../myportfolio.service';

@Component({
  selector: 'app-sell-stock',
  templateUrl: './sell-stock.component.html',
  styleUrls: ['./sell-stock.component.scss']
})
export class SellStockComponent implements OnInit {
  stock: IStock;

  constructor(public myportfolioService: MyportfolioService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private transactionService: TransactionsService,
              private router: Router
  ) { }

ngOnInit(): void {
  this.loadStock();
  }

  loadStock() {
    return this.myportfolioService.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.stock = response;
     // this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });
  }
}









