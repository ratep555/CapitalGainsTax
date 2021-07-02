import { DecimalPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { IProfitTotal } from 'src/app/shared/models/profitTotal';
import { INewSurtax, INewSurtax1, ISurtax } from 'src/app/shared/models/surtax';
import { SurtaxService } from 'src/app/surtax/surtax.service';
import { TaxliabilityService } from '../taxliability.service';


@Component({
  selector: 'app-tax-liability-detail',
  templateUrl: './tax-liability-detail.component.html',
  styleUrls: ['./tax-liability-detail.component.scss']
})
export class TaxLiabilityDetailComponent implements OnInit {

  profit: IProfitTotal;
  profit1: IProfitTotal;
  surtaxes: INewSurtax[];
  surtaxes1: INewSurtax1[] = [];
  surtax: INewSurtax;
  novi: INewSurtax1;

  surtaxForm: FormGroup;

  selectedValue: string;
  selectedOption: any;
  countries: any[] = [
    {
      name: 'Afghanistan',
      phoneCode: '+93',
      alpha2code: 'AF',
      alpha3code: 'AFG'
    },
    {
      name: 'Albania',
      phoneCode: '+93',
      alpha2code: 'AF',
      alpha3code: 'AFG'
    },
    {
      name: 'Algeria',
      phoneCode: '+93',
      alpha2code: 'AF',
      alpha3code: 'AFG'
    }];


  constructor(private taxLiabilityService: TaxliabilityService,
              private surtaxService: SurtaxService,
              private formBuilder: FormBuilder,
              private router: Router) { }


              selected: string;
              states: string[] = [
                'Alabama',
                'Alaska',
                'Arizona',
                'Arkansas',
                'Rhode Island',
                'South Carolina',
                'South Dakota',
                'Tennessee',
                'Texas',
                'Utah',
                'Vermont',
                'Virginia',
                'Washington',
                'West Virginia',
                'Wisconsin',
                'Wyoming'
              ];

  ngOnInit(): void {
    this.surtaxForm = this.formBuilder.group({
      id: [null],
      residence: [null]
    });

    this.showProfit();
    this.showSurtaxes();
    this.showAnotherSurtax();
  }

  showProfit() {
    this.taxLiabilityService.showTaxLiability1().subscribe(response => {
      this.profit = response;
    }, error => {
      console.log(error);
    });
  }

  showProfit1() {
    this.taxLiabilityService.showTaxLiability3(this.surtaxForm.get('id').value).subscribe(response => {
      this.profit1 = response;
    }, error => {
      console.log(error);
    });
  }

showSurtaxes() {
  this.surtaxService.getCoreSurtaxes().subscribe(response => {
      this.surtaxes = response;
     // this.surtaxes1 = response;
  }, error => {
    console.log(error);
  });
}

onSelect(event: TypeaheadMatch): void {
  this.selectedOption = event.item;
}

onSelect2(event: TypeaheadMatch): void {
  const newSurtax: INewSurtax1 = event.item;
  this.surtaxForm.patchValue({
    id: newSurtax.id
  });
}

showAnotherSurtax() {
  this.taxLiabilityService.showTaxLiability3(this.surtaxForm.get('id').value).subscribe(response => {
    this.profit1 = response;
 },
 error => {
   console.log(error);
 });
}

onSubmit() {
   this.taxLiabilityService.showTaxLiability3(this.surtaxForm.get('id').value).subscribe(response => {
     this.profit1 = response;
  },
  error => {
    console.log(error);
  });
}

}





