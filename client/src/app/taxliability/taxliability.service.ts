import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { IAnnual } from '../shared/models/annual';
import { IProfit } from '../shared/models/profit';
import { IProfitTotal } from '../shared/models/profitTotal';
import { ITaxliability } from '../shared/models/taxliability';

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

  showTaxLiability5(id: number) {
    return this.http.put(this.baseUrl + 'taxLiability/up/' + id, {});
  }

  showTaxLiability7(id: number) {
    return this.http.put<IAnnual>(this.baseUrl + 'taxLiability/annual/' + id, {});
  }

  giveAnnual() {
    return this.http.get<IAnnual>(this.baseUrl + 'taxLiability/annualidemo/');
  }

  showTaxLiability4() {
    return this.http.get<ITaxliability>(this.baseUrl + 'taxLiability');
  }

  showTaxLiability2(values: any) {
    return this.http.put<IProfitTotal>(this.baseUrl + 'transactions/profitwowz', values);
  }
}








