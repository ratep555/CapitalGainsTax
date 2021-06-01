import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { IStock } from 'src/app/shared/models/stock';
import { IStTransaction } from 'src/app/shared/models/transaction';
import { TransactionsService } from 'src/app/transactions/transactions.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-sell-stock-reactive-form',
  templateUrl: './sell-stock-reactive-form.component.html',
  styleUrls: ['./sell-stock-reactive-form.component.scss']
})
export class SellStockReactiveFormComponent implements OnInit {
  loginForm: FormGroup;
  stock: IStock;
  quantitivo: number;

  constructor(public service: StocksService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private transactionService: TransactionsService,
              private router: Router
    ) { }

  ngOnInit(): void {
    this.loadStock();
    this.loadStock1();
    this.createLoginForm();
  }


  createLoginForm() {
    this.loginForm = new FormGroup({
    price: new FormControl('', Validators.required),
    quantity: new FormControl('', [Validators.required],
    [this.validateQuantity()])
  });
}

  get f() { return this.loginForm.controls; }


onSubmit() {
  this.service.formData.stockId = this.stock.id;
  this.service.sellStock1(this.loginForm.value).subscribe(() => {
    this.resetForm(this.loginForm);
    this.router.navigateByUrl('myportfolio');

  },
  error => {
    console.log(error);
  });
 // this.router.navigateByUrl('stocks');
}

  loadStock() {
    return this.service.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
      this.stock = response;
     // this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });
  }

  loadStock1() {
    return this.service.getStock1(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe((response: number) => {
      this.quantitivo = response;
     // this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.service.formData = new IStTransaction();
  }

  validateQuantity(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.service.checkQuantity(control.value).pipe(
            map(res => {
              return res ? { emailExists1: true } : null;
            })
          );
        })
      );
    };
    this.service.formData.stockId = this.stock.id;

  }

}








