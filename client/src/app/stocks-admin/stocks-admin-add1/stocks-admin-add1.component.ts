import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/shared/models/category';
import { ICountry } from 'src/app/shared/models/country';
import { StocksAdminService } from '../stocks-admin.service';
import { INewStockToCreate } from 'src/app/shared/models/stockToCreate';


@Component({
  selector: 'app-stocks-admin-add1',
  templateUrl: './stocks-admin-add1.component.html',
  styleUrls: ['./stocks-admin-add1.component.scss']
})
export class StocksAdminAdd1Component implements OnInit {
  stockForms: FormArray = this.fb.array([]);
  categoryList = [];
  countryList = [];
  notification = null;

  constructor(private stockAdminService: StocksAdminService,
              private router: Router,
              private fb: FormBuilder) { }

  ngOnInit(): void {
    this.stockAdminService.getCategories()
    .subscribe(res => this.categoryList = res as []);
    this.stockAdminService.getCountries()
    .subscribe(res => this.countryList = res as []);
    this.addStockForm();
  }

  addStockForm() {
    this.stockForms.push(this.fb.group({
      id: [0],
      symbol: ['', Validators.required],
      currentPrice: ['', Validators.required],
      companyName: ['', Validators.required],
      countryId: [0, Validators.min(1)],
      categoryId: [0, Validators.min(1)],
    }));
  }

  get f() { return this.stockForms.controls; }


  recordSubmit(fg: FormGroup) {
      this.stockAdminService.postStock(fg.value).subscribe(
        (res: any) => {
          fg.patchValue({ id: res.id });
          this.router.navigateByUrl('stocksAdmin');
        }, error => {
            console.log(error);
          });
        }


}








