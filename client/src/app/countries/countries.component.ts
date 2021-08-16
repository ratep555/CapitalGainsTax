import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ICountry } from '../shared/models/country';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { CountriesService } from './countries.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';


@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  countries: ICountry[];
  myParams = new MycategoryParams();
  totalCount: number;

  constructor(private countryService: CountriesService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.getCountries();
  }

  getCountries() {
    this.countryService.getCountries(this.myParams)
    .subscribe(response => {
      this.countries = response.data;
      this.myParams.pageNumber = response.pageIndex;
      this.myParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getCountries();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MycategoryParams();
    this.getCountries();
  }

  onPageChanged(event: any) {
    if (this.myParams.pageNumber !== event) {
      this.myParams.pageNumber = event;
      this.getCountries();
    }
}

/* onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.countryService.deleteCountry(id)
      .subscribe(
        res => {
          this.getCountries();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
} */

onDelete(id: number) {
    Swal.fire({
      title: 'Are you sure want to delete this record?',
      text: 'You will not be able to recover it afterwards!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
          this.countryService.deleteCountry(id)
      .subscribe(
        res => {
          this.getCountries();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
});
}
}
