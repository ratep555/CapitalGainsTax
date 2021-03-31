import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { error } from 'selenium-webdriver';
import { IStock } from 'src/app/shared/models/stock';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})
export class StockDetailsComponent implements OnInit {
  stock: IStock;

  constructor(private stocksService: StocksService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadStock();
  }

  loadStock() {
    return this.stocksService.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.stock = response;
    }, error => {
      console.log(error);
    });

  }

}
