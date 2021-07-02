import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IPagination5 } from '../shared/models/pagination5';
import { ISurtax, INewSurtax } from '../shared/models/surtax';

@Injectable({
  providedIn: 'root'
})
export class SurtaxService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getSurtaxes(mycategoryParams: MycategoryParams) {
    let params = new HttpParams();
    if (mycategoryParams.query) {
      params = params.append('query', mycategoryParams.query);
    }
    params = params.append('page', mycategoryParams.pageNumber.toString());
    params = params.append('pageCount', mycategoryParams.pageSize.toString());
    return this.http.get<IPagination5>(this.baseUrl + 'surtaxes', {observe: 'response', params})
    .pipe(
      map(response  => {
        return response.body;
      })
    );
  }

  getCoreSurtaxes() {
    return this.http.get<ISurtax[]>(this.baseUrl + 'surtaxes/gethim');
  }
}








