import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IStock } from 'src/app/shared/models/stock';
import { IStTransaction } from 'src/app/shared/models/transaction';
import { TransactionsService } from 'src/app/transactions/transactions.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { StocksService } from '../stocks.service';

@Component({
  selector: 'app-add-stock-reactive-form',
  templateUrl: './add-stock-reactive-form.component.html',
  styleUrls: ['./add-stock-reactive-form.component.scss']
})
export class AddStockReactiveFormComponent implements OnInit {
  loginForm: FormGroup;
  stock: IStock;

  constructor(private service: StocksService,
              private activatedRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private transactionService: TransactionsService,
              private router: Router
    ) { }

  ngOnInit(): void {
    this.loadStock();
    this.createLoginForm();
  }

createLoginForm() {
    this.loginForm = new FormGroup({
    price: new FormControl('', [Validators.required]),
    quantity: new FormControl('', Validators.required),
    buyingDate: new FormControl('', Validators.required)
  });
}

onSubmit() {
  this.service.formData.stockId = this.stock.id;
  this.service.buyStock1(this.loginForm.value).subscribe(() => {
    this.resetForm(this.loginForm);
    this.router.navigateByUrl('myportfolio');
  },
  error => {
    console.log(error);
  });
}

loadStock() {
  return this.service.getStock(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(response => {
    this.stock = response;
   // this.bcService.set('@stockDetails', this.stock.companyName);
  }, error => {
    console.log(error);
  });
}

resetForm(form: FormGroup) {
  form.reset();
  this.service.formData = new IStTransaction();
}
}



