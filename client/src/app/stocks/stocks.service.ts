import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategory } from '../shared/models/category';
import { ICountry } from '../shared/models/country';
import { IPagination } from '../shared/models/pagination';
import { StockParams } from '../shared/models/stockParams';
import {map} from 'rxjs/operators';
import { IStock } from '../shared/models/stock';
import { environment } from 'src/environments/environment';
import { IStTransaction } from '../shared/models/transaction';
import { Router } from '@angular/router';
import { IStockToCreate } from '../shared/models/stockToCreate';


@Injectable({
  providedIn: 'root'
})
export class StocksService {
  baseUrl = environment.apiUrl;
  baseUrlTup = 'https://localhost:5001/api/transactions/kreativo';
  baseUrlTup1 = 'https://localhost:5001/api/transactions/kreativissimo';
  baseUrlTup2 = 'https://localhost:5001/api/transactions/exceed';
  formData: IStTransaction = new IStTransaction();


  constructor(private http: HttpClient) { }

  buyStock() {
    return this.http.post(`${this.baseUrlTup}/${this.formData.stockId}`, this.formData);
  }

  buyStock1(values: any) {
    return this.http.post(`${this.baseUrlTup}/${this.formData.stockId}`, values);
  }

  sellStock() {
    return this.http.post(`${this.baseUrlTup1}/${this.formData.stockId}`, this.formData);
  }

  checkQuantity(quantity: string) {
    return this.http.get(`${this.baseUrlTup2}/${this.formData.id}/${this.formData.stockId}/exceed?quantiy=` + quantity);
  }
  // ovo koristiš, newyear je samo za probu! - sad si ga stavio za stalno pa kaj bude:)
  sellStock1(values: any) {
    return this.http.post(`${this.baseUrlTup1}/${this.formData.stockId}`, values);
  }
  // samo si mijenjao kreativisiimo1 u 2 pa si vratio
  sellStock1NewYear(values: any) {
    return this.http.post(`${this.baseUrl}transactions/kreativissimo1/${this.formData.stockId}`, values);
  }

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

  getStock2(id: number) {
    return this.http.get<IStockToCreate>(this.baseUrl + 'stocksAdmin/' + id);
  }

  // ovo je da samo vraća totalQuantity
  getStock1(id: number) {
    return this.http.get(this.baseUrl + 'stocks/ajmoopet/' + id);
  }

  // ovo je najbolje, sve bez specifikacija
  getStock3(id: number) {
    return this.http.get<IStock>(this.baseUrl + 'stocks/aj/' + id);
  }

  getCategories() {
    return this.http.get<ICategory[]>(this.baseUrl + 'stocks/categories');
  }

  getCountries() {
    return this.http.get<ICountry[]>(this.baseUrl + 'stocks/countries');
  }

}




