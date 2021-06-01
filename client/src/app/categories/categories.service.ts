import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ICategory, INewCategory } from '../shared/models/category';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination1 } from '../shared/models/pagination1';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  baseUrl = environment.apiUrl;
  formData: INewCategory = new INewCategory();
  list: INewCategory[];
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };


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

  createCategory(values: any) {
    return this.http.post(this.baseUrl + 'categories/create', values);
  }

  updateCategory(values: any) {
    return this.http.put<INewCategory>(`${this.baseUrl}categories/${this.formData.id}`, values);
  }

  getCategoryById(id: number) {
    return this.http.get(this.baseUrl + 'categories/' + id)
      .pipe(map(
        response => {
          return response;
        }));
  }

  updateCategory1(category: INewCategory) {
    return this.http.put<INewCategory>(this.baseUrl + 'categories/edit', category)
    .pipe(map(
      response => {
        return response;
      }));
}

 updateCategory2(id, player): Observable<ICategory> {
    return this.http.put<ICategory>(this.baseUrl + 'categories/rez' + id, JSON.stringify(player), this.httpOptions)
      .pipe(
        catchError(this.errorHandler)
      );
  }

  updateCategory3(id: number, params: any) {
    return this.http.put(`${this.baseUrl}categories/${id}`, params);
  }

  getById(id: number) {
    return this.http.get<ICategory>(`${this.baseUrl}categories/${id}`);
  }

  getCategory2(id): Observable<ICategory> {
    return this.http.get<ICategory>(this.baseUrl + 'categories/' + id)
      .pipe(
        catchError(this.errorHandler)
      );
  }


  getCategory(id: number) {
    return this.http.get<ICategory>(this.baseUrl + 'categories/' + id);
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }

  deleteCategory(id: number) {
    return this.http.delete(`${this.baseUrl}categories/${id}`);
}

refreshList() {
  this.http.get(`${this.baseUrl}categories`)
    .toPromise()
    .then(res => this.list = res as INewCategory[]);
}
}


