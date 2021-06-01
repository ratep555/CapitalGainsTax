import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { INewStock, IStock } from 'src/app/shared/models/stock';
import { INewStockToCreate, IStockToCreate } from 'src/app/shared/models/stockToCreate';
import { StocksService } from 'src/app/stocks/stocks.service';
import { StocksAdminService } from '../stocks-admin.service';

@Component({
  selector: 'app-stocks-admin-edit',
  templateUrl: './stocks-admin-edit.component.html',
  styleUrls: ['./stocks-admin-edit.component.scss']
})
export class StocksAdminEditComponent implements OnInit {
  stockForms: FormArray = this.fb.array([]);
  categoryList = [];
  countryList = [];
  id: number;
  stock: IStockToCreate;

  constructor(private stockAdminService: StocksAdminService,
              private stockService: StocksService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];

    this.stockAdminService.getCategories()
    .subscribe(res => this.categoryList = res as []);
    this.stockAdminService.getCountries()
    .subscribe(res => this.countryList = res as []);

    /* this.stockAdminService.getStockList().subscribe(
  (stockAccount: any) => {
    this.stockForms.push(this.fb.group({
              id: [this.id],
              symbol: [stockAccount.symbol, Validators.required],
              currentPrice: [stockAccount.currentPrice, Validators.required],
              companyName: [stockAccount.companyName, Validators.required],
              countryId: [stockAccount.countryId, Validators.min(1)],
              categoryId: [stockAccount.categoryId, Validators.required]
            }));
      }); */
    this.stockService.getStock2(this.id).subscribe(
  (stock: INewStockToCreate) => {
    this.stockForms.push(this.fb.group({
              id: [this.id],
              symbol: [stock.symbol, Validators.required],
              currentPrice: [stock.currentPrice, Validators.required],
              companyName: [stock.companyName, Validators.required],
              countryId: [stock.countryId, Validators.min(1)],
              categoryId: [stock.categoryId, Validators.required]
            }));
      });
  }

  recordSubmit(fg: FormGroup) {
      this.stockAdminService.putStock(fg.value).subscribe(
        (res: any) => {
          this.router.navigateByUrl('stocksAdmin');
        }, error => {
            console.log(error);
          });
        }

}



