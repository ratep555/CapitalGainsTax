import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { first } from 'rxjs/operators';
import { ICategory } from '../shared/models/category';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { CategoriesService } from './categories.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  categories: ICategory[];
  mycategoryParams = new MycategoryParams();
  totalCount: number;



  constructor(private categoryService: CategoriesService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.getCategories();
   // this.categoryService.refreshList();

  }

  getCategories() {
    this.categoryService.getCategories(this.mycategoryParams)
    .subscribe(response => {
      this.categories = response.data;
      this.mycategoryParams.pageNumber = response.pageIndex;
      this.mycategoryParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.mycategoryParams.query = this.searchTerm.nativeElement.value;
    this.getCategories();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.mycategoryParams = new MycategoryParams();
    this.getCategories();
  }

  onPageChanged(event: any) {
    if (this.mycategoryParams.pageNumber !== event) {
      this.mycategoryParams.pageNumber = event;
      this.getCategories();
    }
}

onDelete(id: number) {
  if (confirm('Are you sure you want to delete this record?')) {
    this.categoryService.deleteCategory(id)
      .subscribe(
        res => {
          this.getCategories();
         // this.categoryService.refreshList();
          this.toastr.error('Deleted successfully!');
        },
        err => { console.log(err);
         }
      );
  }
}
}






