import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IPortfolioAccount } from '../shared/models/portfolioAccount';

@Injectable({
  providedIn: 'root'
})
export class MyportfolioService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getPortfolioAccount() {
    return this.http.get(this.baseUrl + 'portfolioAccounts');
  }
}
