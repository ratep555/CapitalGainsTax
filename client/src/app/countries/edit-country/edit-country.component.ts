import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { CountriesService } from '../countries.service';

@Component({
  selector: 'app-edit-country',
  templateUrl: './edit-country.component.html',
  styleUrls: ['./edit-country.component.scss']
})
export class EditCountryComponent implements OnInit {
  countryForm: FormGroup;
  id: number;


  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private countryService: CountriesService
   ) { }

   ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['id'];

    this.countryForm = this.formBuilder.group({
      id: [this.id],
      countryName: new FormControl('', [Validators.required])
        });

    this.countryService.getCountryById(this.id)
    .pipe(first())
    .subscribe(x => this.countryForm.patchValue(x));
  }


  onSubmit() {
    if (this.countryForm.invalid) {
        return;
    }
    this.updateCountry();
}

private updateCountry() {
  this.countryService.updateCountry(this.id, this.countryForm.value)
      .pipe(first())
      .subscribe(() => {
          this.router.navigateByUrl('countries');
        }, error => {
          console.log(error);
        });
      }
}

