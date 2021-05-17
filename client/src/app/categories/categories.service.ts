import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICategory } from '../shared/models/category';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination } from '../shared/models/pagination';
import { IPagination1 } from '../shared/models/pagination1';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getCategories(mycategoryParams: MycategoryParams) {
    let params = new HttpParams();
    if (mycategoryParams.query) {
      params = params.append('query', mycategoryParams.query);
    }
    params = params.append('page', mycategoryParams.pageNumber.toString());
    params = params.append('pageCount', mycategoryParams.pageSize.toString());
    return this.http.get<IPagination1>(this.baseUrl + 'categories/pagingtup', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  buyStock1(values: any) {
    return this.http.post(this.baseUrl + 'categories/create', values);
  }

}


