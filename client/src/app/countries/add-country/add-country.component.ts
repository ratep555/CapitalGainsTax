import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewCountry } from 'src/app/shared/models/country';
import { CountriesService } from '../countries.service';

@Component({
  selector: 'app-add-country',
  templateUrl: './add-country.component.html',
  styleUrls: ['./add-country.component.scss']
})
export class AddCountryComponent implements OnInit {
  countryForm: FormGroup;


  constructor(private countryService: CountriesService, private router: Router) { }

  ngOnInit(): void {
    this.createCountryForm();
  }

  createCountryForm() {
    this.countryForm = new FormGroup({
      countryName: new FormControl('', [Validators.required])
    });
  }

  onSubmit() {
    this.countryService.createCountry(this.countryForm.value).subscribe(() => {
      this.resetForm(this.countryForm);
      this.router.navigateByUrl('countries');
    },
    error => {
      console.log(error);
    });
  }


  resetForm(form: FormGroup) {
    form.reset();
    this.countryService.formData = new INewCountry();
  }
}









