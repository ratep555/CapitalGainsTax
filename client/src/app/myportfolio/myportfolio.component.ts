import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MyportfolioParams } from '../shared/models/myportfolioParams';
import { IPortfolioAccount } from '../shared/models/portfolioAccount';
import { MyportfolioService } from './myportfolio.service';

@Component({
  selector: 'app-myportfolio',
  templateUrl: './myportfolio.component.html',
  styleUrls: ['./myportfolio.component.scss']
})
export class MyportfolioComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  @ViewChild('searchTu', {static: false}) searchTermTu: ElementRef;
  myportfolioParams = new MyportfolioParams();
  portfolioAccount: IPortfolioAccount[];
  total = 0;
  total1 = 0;
  private value;

  constructor(private myportfolioService: MyportfolioService) { }

  ngOnInit(): void {
   this.getPortfolioAccounts();
  }

  getPortfolioAccounts() {
    this.myportfolioService.getPortfolioAccount(this.myportfolioParams)
    .subscribe((portfolioAccount: IPortfolioAccount[]) => {
      this.portfolioAccount = portfolioAccount;
      this.findsum(this.portfolioAccount);
    }, error => console.log(error));
  }

  findsum(data){
    this.value = data;
    console.log(this.value);
    for (let j = 0; j < data.length; j++){
         this.total += (this.value[j].totalQuantity * this.value[j].averagePriceOfPurchase);
         this.total1 += (this.value[j].totalQuantity * this.value[j].currentPrice);
         console.log(this.total);
    }
  }


  onSearchUser() {
    this.myportfolioParams.query = this.searchTermTu.nativeElement.value;
    this.getPortfolioAccounts();
  }

  onSearch() {
    this.myportfolioParams.query = this.searchTerm.nativeElement.value;
    this.getPortfolioAccounts();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    // we are reseting all of our filters
    this.myportfolioParams = new MyportfolioParams();
    // we need our unfiltered list of products
    this.getPortfolioAccounts();
  }

  onResetTu() {
    this.searchTermTu.nativeElement.value = '';
    // we are reseting all of our filters
    this.myportfolioParams = new MyportfolioParams();
    // we need our unfiltered list of products
    this.getPortfolioAccounts();
  }
}









