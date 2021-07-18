import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChartsService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProfitOrLosses() {
    return this.http.get<any>(this.baseUrl + 'charts').pipe(
    map( result => {
      console.log(result);
      return result;
    })
    );
  }
}
