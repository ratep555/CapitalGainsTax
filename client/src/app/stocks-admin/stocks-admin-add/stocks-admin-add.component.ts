import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ICategory } from 'src/app/shared/models/category';
import { ICountry } from 'src/app/shared/models/country';
import { INewStock } from 'src/app/shared/models/stock';
import { StocksAdminService } from '../stocks-admin.service';

@Component({
  selector: 'app-stocks-admin-add',
  templateUrl: './stocks-admin-add.component.html',
  styleUrls: ['./stocks-admin-add.component.scss']
})
export class StocksAdminAddComponent implements OnInit {
  countries: ICountry[];
  categories: ICategory[];
  stockForm: FormGroup;


  constructor(private stockAdminService: StocksAdminService,
              private router: Router,
              private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.createStockForm();
    this.getCategories();
    this.getCountries();
  }

  get f() { return this.stockForm.controls; }


  createStockForm() {
    this.stockForm = new FormGroup({
      symbol: new FormControl('', [Validators.required]),
      currentPrice: new FormControl('', [Validators.required]),
      companyName: new FormControl('', [Validators.required]),
      country: new FormControl('', [Validators.required]),
      category: new FormControl('', [Validators.required])
    });
  }



  onSubmit() {
      this.stockAdminService.createStock(this.stockForm.value).subscribe(() => {
      this.resetForm(this.stockForm);
      this.router.navigateByUrl('stocksAdmin');
    },
    error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.stockAdminService.formData = new INewStock();
  }

  getCategories() {
    this.stockAdminService.getCategories().subscribe(response => {
      this.categories = response;
    }, error => {
      console.log(error);
    }
    );
  }

  getCountries() {
    this.stockAdminService.getCountries().subscribe(response => {
      this.countries = response;
    }, error => {
      console.log(error);
    }
    );
  }
}




