import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategory } from '../shared/models/category';
import { ICountry } from '../shared/models/country';
import { IPagination } from '../shared/models/pagination';
import { StockParams } from '../shared/models/stockParams';
import {map} from 'rxjs/operators';
import { IStock } from '../shared/models/stock';


@Injectable({
  providedIn: 'root'
})
export class StocksService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getStocks(stockParams: StockParams) {
    let params = new HttpParams();

    if (stockParams.search) {
      params = params.append('search', stockParams.search);
    }

    params = params.append('sort', stockParams.sort);
    params = params.append('pageIndex', stockParams.pageNumber.toString());
    params = params.append('pageSize', stockParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'stocks', {observe: 'response', params})
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  getStock(id: number) {
    return this.http.get<IStock>(this.baseUrl + 'stocks/' + id);
  }

  getCategories() {
    return this.http.get<ICategory[]>(this.baseUrl + 'stocks/categories');
  }

  getCountries() {
    return this.http.get<ICountry[]>(this.baseUrl + 'stocks/countries');
  }

}




