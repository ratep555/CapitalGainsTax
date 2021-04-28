import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TransactionParams } from '../shared/models/transactionParams';
import {map} from 'rxjs/operators';
import { IPagination } from '../shared/models/pagination';
import { IPaginationTransaction } from '../shared/models/paginationForTransaction';
import { TransactionsForUserParams } from '../shared/models/transactionsForUserParams';
import { IStockTransaction, IStTransaction } from '../shared/models/transaction';


@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  baseUrl = environment.apiUrl;
  formData: IStTransaction = new IStTransaction();
  list: IStockTransaction[];

  constructor(private http: HttpClient) { }

  getTransactionsForUser(transactionsForUserParams: TransactionsForUserParams) {
    let params = new HttpParams();
    if (transactionsForUserParams.query) {
      params = params.append('query', transactionsForUserParams.query);
    }
    return this.http.get(this.baseUrl + 'transactions/pekidreki', {observe: 'response', params})
    .pipe(
    map(response => {
    return response.body;
        })
      );
    }

   getTransactions(transactionParams: TransactionParams) {
   let params = new HttpParams();
   if (transactionParams.stockId !== 0) {
     params = params.append('stockId', transactionParams.stockId.toString());
   }
   if (transactionParams.search) {
      params = params.append('search', transactionParams.search);
    }
   params = params.append('sort', transactionParams.sort);
   params = params.append('pageIndex', transactionParams.pageNumber.toString());
   params = params.append('pageSize', transactionParams.pageSize.toString());

   return this.http.get<IPaginationTransaction>(this.baseUrl + 'transactions', {observe: 'response', params})
  .pipe(
  map(response => {
  return response.body;
      })
    );
  }

  createTransaction() {
    return this.http.post(this.baseUrl + 'transactions', this.formData);
  }

  sellStock() {
    this.formData.purchase = false;
    return this.http.post(this.baseUrl + 'transactions', this.formData);
  }

  refreshList() {
    this.http.get(this.baseUrl + 'transactions')
      .toPromise()
      .then(res => this.list = res as IStockTransaction[]);
  }
}







