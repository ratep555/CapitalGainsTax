import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'selenium-webdriver';
import { IStock } from 'src/app/shared/models/stock';
import { IStTransaction } from 'src/app/shared/models/transaction';
import { BreadcrumbService } from 'xng-breadcrumb';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-stock-details',
  templateUrl: './stock-details.component.html',
  styleUrls: ['./stock-details.component.scss']
})
export class StockDetailsComponent implements OnInit {
  stock: IStock;

  constructor(public stocksService: StocksService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadStock();
  }

  onSubmit(form: NgForm) {
    this.stocksService.formData.stockId = this.stock.id;
    this.buyingStock(form);
}

  buyingStock(form: NgForm) {
    this.stocksService.buyStock().subscribe(
      response => {
      //  this.stock.id = this.stocksService.formData.stockId;
        this.resetForm(form);
        this.router.navigateByUrl('stocks');
      }, error => {
        console.log(error);
      }
     );
   }


  resetForm(form: NgForm) {
    form.form.reset();
    this.stocksService.formData = new IStTransaction();
  }

  loadStock() {
    return this.stocksService.getStock3(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.stock = response;
      this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });

  }

}
