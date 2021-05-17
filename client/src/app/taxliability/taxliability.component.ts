import { DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IProfit } from '../shared/models/profit';
import { IProfitTotal } from '../shared/models/profitTotal';
import { TaxliabilityService } from './taxliability.service';

@Component({
  selector: 'app-taxliability',
  templateUrl: './taxliability.component.html',
  styleUrls: ['./taxliability.component.scss']
})
export class TaxliabilityComponent implements OnInit {
  profit: IProfit;
  profit1: IProfitTotal;
  tax: DecimalPipe;


  constructor(private service: TaxliabilityService) { }

  ngOnInit(): void {
    this.showProfit();
    this.showProfit1();
  }

  showProfit() {
this.service.showTaxLiability().subscribe(response => {
  this.profit = response;
});
  }

  showProfit1() {
this.service.showTaxLiability1().subscribe(response => {
  this.profit1 = response;
});
  }

}




