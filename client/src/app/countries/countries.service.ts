import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination2 } from '../shared/models/pagination2';
import { ICountry, INewCountry } from '../shared/models/country';


@Injectable({
  providedIn: 'root'
})
export class CountriesService {
  baseUrl = environment.apiUrl;
  formData: INewCountry = new INewCountry();


  constructor(private http: HttpClient) { }

  getCountries(mycategoryParams: MycategoryParams) {
    let params = new HttpParams();
    if (mycategoryParams.query) {
      params = params.append('query', mycategoryParams.query);
    }
    params = params.append('page', mycategoryParams.pageNumber.toString());
    params = params.append('pageCount', mycategoryParams.pageSize.toString());
    return this.http.get<IPagination2>(this.baseUrl + 'countries/novi', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  createCountry(values: any) {
    return this.http.post(this.baseUrl + 'countries/create', values);
  }

  updateCountry(id: number, params: any) {
    return this.http.put(`${this.baseUrl}countries/${id}`, params);
  }

  getCountryById(id: number) {
    return this.http.get<ICountry>(`${this.baseUrl}countries/${id}`);
  }

  deleteCountry(id: number) {
    return this.http.delete(`${this.baseUrl}countries/${id}`);
}

}









