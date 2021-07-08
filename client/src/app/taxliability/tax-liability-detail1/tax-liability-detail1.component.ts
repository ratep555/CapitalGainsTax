import { Component, OnInit } from '@angular/core';
import { ITaxliability } from 'src/app/shared/models/taxliability';
import { DecimalPipe } from '@angular/common';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead';
import { SurtaxService } from 'src/app/surtax/surtax.service';
import { TaxliabilityService } from '../taxliability.service';
import { INewSurtax, INewSurtax1 } from 'src/app/shared/models/surtax';


@Component({
  selector: 'app-tax-liability-detail1',
  templateUrl: './tax-liability-detail1.component.html',
  styleUrls: ['./tax-liability-detail1.component.scss']
})
export class TaxLiabilityDetail1Component implements OnInit {
taxliability: ITaxliability;
// probaj tu umjesto any staviti array of surtaxes
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

    this.gettingTaxLiability();
    this.showSurtaxes();
  }

  gettingTaxLiability() {
    this.taxLiabilityService.showTaxLiability4().subscribe(response => {
      this.taxliability = response;
    }, error => {
      console.log(error);
    });
  }

  showSurtaxes() {
    this.surtaxService.getCoreSurtaxes().subscribe(response => {
        this.surtaxes = response;
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
    this.taxLiabilityService.showTaxLiability5(this.surtaxForm.get('id').value).subscribe(response => {
      this.gettingTaxLiability();
   },
   error => {
     console.log(error);
   });
 }

  }


