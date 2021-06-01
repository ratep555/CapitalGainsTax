import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICategory } from '../shared/models/category';
import { ICountry } from '../shared/models/country';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination2 } from '../shared/models/pagination2';
import { IPagination3 } from '../shared/models/pagination3';
import { INewStock, IStock } from '../shared/models/stock';
import { INewStockToCreate } from '../shared/models/stockToCreate';

@Injectable({
  providedIn: 'root'
})
export class StocksAdminService {
  baseUrl = environment.apiUrl;
  formData: INewStock = new INewStock();
  formData1: INewStockToCreate = new INewStockToCreate();

  constructor(private http: HttpClient) { }

  getStocks(myParams: MycategoryParams) {
    let params = new HttpParams();
    if (myParams.query) {
      params = params.append('query', myParams.query);
    }
    params = params.append('page', myParams.pageNumber.toString());
    params = params.append('pageCount', myParams.pageSize.toString());
    return this.http.get<IPagination3>(this.baseUrl + 'stocksAdmin', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createStock(values: any) {
    return this.http.post(this.baseUrl + 'stocksAdmin/createi', values);
  }

  createStock1(values: any) {
    return this.http.post(this.baseUrl + 'stocksAdmin/pikilili', values);
  }

  updateStock(id: number, params: any) {
    return this.http.put(`${this.baseUrl}countries/${id}`, params);
  }

  getStockById(id: number) {
    return this.http.get<IStock>(`${this.baseUrl}countries/${id}`);
  }

  deleteStock(id: number) {
    return this.http.delete(`${this.baseUrl}countries/${id}`);
}

getCategories() {
  return this.http.get<ICategory[]>(this.baseUrl + 'stocksAdmin/categories');
}

getCountries() {
  return this.http.get<ICountry[]>(this.baseUrl + 'stocksAdmin/countries');
}

postStock(formDatal) {
  return this.http.post(this.baseUrl + 'stocksAdmin/pikilili', formDatal);
}

putStock(formData) {
  return this.http.put(environment.apiUrl + 'stocksAdmin/' + formData.id, formData);
}

getStockList() {
  return this.http.get(environment.apiUrl + 'stocksAdmin/getter');
}

deletingStock(id) {
  return this.http.delete(environment.apiUrl + 'stocksAdmin/' + id);
}
}





