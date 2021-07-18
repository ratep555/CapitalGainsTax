import { Component, OnInit } from '@angular/core';
import { IAnnual } from 'src/app/shared/models/annual';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { SurtaxService } from 'src/app/surtax/surtax.service';
import { TaxliabilityService } from '../taxliability.service';
import { INewSurtax, INewSurtax1 } from 'src/app/shared/models/surtax';

@Component({
  selector: 'app-tax-liability-detail2',
  templateUrl: './tax-liability-detail2.component.html',
  styleUrls: ['./tax-liability-detail2.component.scss']
})
export class TaxLiabilityDetail2Component implements OnInit {
  annual: IAnnual;
  selectedOption: any;
 surtaxes: INewSurtax[];
 surtaxForm: FormGroup;

 constructor(private taxLiabilityService: TaxliabilityService,
             private surtaxService: SurtaxService,
             private formBuilder: FormBuilder,
             private router: Router) { }

  ngOnInit(): void {
    this.surtaxForm = this.formBuilder.group({
      id: [null],
      residence: [null]
    });
    this.showSurtaxes();
    this.showAnnual1();
  }

  showSurtaxes() {
    this.surtaxService.getCoreSurtaxes().subscribe(response => {
        this.surtaxes = response;
    }, error => {
      console.log(error);
    });
  }

  showAnnual() {
    this.taxLiabilityService.showTaxLiability7(this.surtaxForm.get('id').value).subscribe(response => {
        this.annual = response;
    }, error => {
      console.log(error);
    });
  }

  showAnnual1() {
    this.taxLiabilityService.giveAnnual().subscribe(response => {
        this.annual = response;
    }, error => {
      console.log(error);
    });
  }

  onSelect2(event: TypeaheadMatch): void {
    const newSurtax: INewSurtax1 = event.item;
    this.surtaxForm.patchValue({
      id: newSurtax.id
    });
  }

  onSubmit() {
    this.taxLiabilityService.showTaxLiability7(this.surtaxForm.get('id').value).subscribe(response => {
      this.showAnnual();
   },
   error => {
     console.log(error);
   });
 }

}




