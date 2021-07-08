import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ITransactionsForUser, ITransactionsWithProfitAndTraffic } from '../shared/models/transacionsForUser';
import { IStockTransaction } from '../shared/models/transaction';
import { TransactionParams } from '../shared/models/transactionParams';
import { TransactionsForUserParams } from '../shared/models/transactionsForUserParams';
import { TransactionsService } from './transactions.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('searchTu', {static: false}) searchTermTu: ElementRef;
  @ViewChild('searchTup', {static: false}) searchTermTup: ElementRef;

  transactions: IStockTransaction[];
  transactionParams = new TransactionParams();
  totalCount: number;
  transactions1: any[];
  transactionsForUser: ITransactionsForUser[];
  transactionsForUserParams = new TransactionsForUserParams();
  listOfTransactions: ITransactionsForUser[];
  totalNetProfit: number;
  totalTraffic: number;


  constructor(private transactionsService: TransactionsService, private http: HttpClient) { }

  ngOnInit(): void {
 // this.getTransactions();
  this.getTransactionsForUser();
  this.getTransactionsForUser1();
}

getTransactionsForUser() {
  this.transactionsService.getTransactionsForUser(this.transactionsForUserParams).
  subscribe((transacionsForUser: ITransactionsForUser[]) => {
    this.transactionsForUser = transacionsForUser;
  }, error => {
    console.log(error);
  });
}

getTransactionsForUser1() {
  this.transactionsService.getTransactionsForUser1(this.transactionsForUserParams).
  subscribe((data: ITransactionsWithProfitAndTraffic) => {
    this.listOfTransactions = data.listOfTransactions;
    this.totalNetProfit = data.totalNetProfit1;
    this.totalTraffic = data.totalTraffic1;
  }, error => {
    console.log(error);
  });
}

onSearchUser() {
  this.transactionsForUserParams.query = this.searchTermTu.nativeElement.value;
  this.getTransactionsForUser();
}

onSearchUser1() {
  this.transactionsForUserParams.query = this.searchTermTup.nativeElement.value;
  this.getTransactionsForUser1();
}

/* getTransactionsForUser() {
  this.transactionsService.getTransactionsForUser().subscribe((transactionsForUser: ITransactionsForUser[]) => {
    this.transactionsForUser = transactionsForUser;
  }, error => {
    console.log(error);
  });
} */

   getTransactions() {
   this.transactionsService.getTransactions(this.transactionParams).subscribe(response => {
     this.transactions = response.data;
     this.transactionParams.pageNumber = response.pageIndex;
     this.transactionParams.pageSize = response.pageSize;
     this.totalCount = response.count;
   }, error => {
     console.log(error);
   });
  }

  onSearch() {
    this.transactionParams.search = this.searchTerm.nativeElement.value;
    this.getTransactions();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    // we are reseting all of our filters
    this.transactionParams = new TransactionParams();
    // we need our unfiltered list of products
    this.getTransactions();
  }
  onResetTu() {
    this.searchTermTu.nativeElement.value = '';
    // we are reseting all of our filters
    this.transactionsForUserParams = new TransactionsForUserParams();
    // we need our unfiltered list of products
    this.getTransactionsForUser();
  }

  onResetTup() {
    this.searchTermTup.nativeElement.value = '';
    this.transactionsForUserParams = new TransactionsForUserParams();
    this.getTransactionsForUser1();
  }
}






