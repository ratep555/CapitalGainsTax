import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MyportfolioParams } from '../shared/models/myportfolioParams';
import { IPortfolioAccount } from '../shared/models/portfolioAccount';

@Injectable({
  providedIn: 'root'
})
export class MyportfolioService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPortfolioAccount(myportfolioParams: MyportfolioParams) {
    let params = new HttpParams();
    if (myportfolioParams.query) {
      params = params.append('query', myportfolioParams.query);
    }
    return this.http.get(this.baseUrl + 'portfolioAccounts/yes', {observe: 'response', params})
    .pipe(
      map(response => {
      return response.body;
          })
        );
      }
}
