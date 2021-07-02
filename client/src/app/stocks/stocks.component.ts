import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { error } from 'selenium-webdriver';
import { ICategory } from '../shared/models/category';
import { ICountry } from '../shared/models/country';
import { IStock } from '../shared/models/stock';
import { StockParams } from '../shared/models/stockParams';
import { StocksAdminService } from '../stocks-admin/stocks-admin.service';
import { StocksService } from './stocks.service';



@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.scss']
})
export class StocksComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;

  stocks: IStock[];
  categories: ICategory[];
  countries: ICountry[];
  stockParams = new StockParams();
  totalCount: number;
  sortOptions = [
    { value: 'name'},
    { value: 'categoryAsc'}
  ];


  constructor(private stockService: StocksService, private stocksAdmin: StocksAdminService) { }

  ngOnInit(): void {
  this.getStocks();
  this.getCategories();
  this.getCountries();
  this.onRefresh();
  }

  getStocks() {
    this.stockService.getStocks(this.stockParams).subscribe(response => {
    this.stocks = response.data;
    this.stockParams.pageNumber = response.pageIndex;
    this.stockParams.pageSize = response.pageSize;
    this.totalCount = response.count;
     }, error => {
      console.log(error);
    });
  }

  getCategories() {
    this.stockService.getCategories().subscribe(response => {
      this.categories = response;
    }, error => {
      console.log(error);
    }
    );
  }

  getCountries() {
    this.stockService.getCountries().subscribe(response => {
      this.countries = response;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.stockParams.search = this.searchTerm.nativeElement.value;
    this.getStocks();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.stockParams = new StockParams();
    this.getStocks();
  }

  onPageChanged(event: any) {
    if (this.stockParams.pageNumber !== event) {
      this.stockParams.pageNumber = event;
      this.getStocks();
    }
}

onSortSelected(sort: string) {
  this.stockParams.sort = sort;
  this.getStocks();
}

onRefresh() {
  this.stocksAdmin.refreshPrices().subscribe(res => {
   this.getStocks();
  },
  error => {
    console.log(error);
  });
  }

}




