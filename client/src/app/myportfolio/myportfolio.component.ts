import { Component, OnInit } from '@angular/core';
import { IPortfolioAccount } from '../shared/models/portfolioAccount';
import { MyportfolioService } from './myportfolio.service';

@Component({
  selector: 'app-myportfolio',
  templateUrl: './myportfolio.component.html',
  styleUrls: ['./myportfolio.component.scss']
})
export class MyportfolioComponent implements OnInit {
  portfolioAccount: IPortfolioAccount[];

  constructor(private myportfolioService: MyportfolioService) { }

  ngOnInit(): void {
   this.getPortfolioAccounts();
  }

  getPortfolioAccounts() {
    this.myportfolioService.getPortfolioAccount().subscribe((portfolioAccount: IPortfolioAccount[]) => {
      this.portfolioAccount = portfolioAccount;
    }, error => console.log(error));
  }
}
