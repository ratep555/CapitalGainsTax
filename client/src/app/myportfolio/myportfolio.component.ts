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

  constructor(private myportfolioService: MyportfolioService) { }

  ngOnInit(): void {
   this.getPortfolioAccounts();
  }

  getPortfolioAccounts() {
    this.myportfolioService.getPortfolioAccount(this.myportfolioParams)
    .subscribe((portfolioAccount: IPortfolioAccount[]) => {
      this.portfolioAccount = portfolioAccount;
    }, error => console.log(error));
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









