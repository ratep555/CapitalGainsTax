import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { error } from 'selenium-webdriver';
import { IStock } from 'src/app/shared/models/stock';
import { BreadcrumbService } from 'xng-breadcrumb';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})
export class StockDetailsComponent implements OnInit {
  stock: IStock;

  constructor(private stocksService: StocksService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService) { }

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

}
