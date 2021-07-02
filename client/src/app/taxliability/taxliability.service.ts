import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IProfit } from '../shared/models/profit';
import { IProfitTotal } from '../shared/models/profitTotal';

@Injectable({
  providedIn: 'root'
})
export class TaxliabilityService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  showTaxLiability() {
    return this.http.get<IProfit>(this.baseUrl + 'transactions/profit');
  }

  showTaxLiability1() {
    return this.http.get<IProfitTotal>(this.baseUrl + 'transactions/profitwow');
  }

  showTaxLiability3(id: number) {
    return this.http.put<IProfitTotal>(this.baseUrl + 'transactions/profitwowy/' + id, {});
  }

  showTaxLiability2(values: any) {
    return this.http.put<IProfitTotal>(this.baseUrl + 'transactions/profitwowz', values);
  }
}








