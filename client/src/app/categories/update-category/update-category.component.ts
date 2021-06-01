import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ICategory, INewCategory } from 'src/app/shared/models/category';
import { CategoriesService } from '../categories.service';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrls: ['./update-category.component.scss']
})
export class UpdateCategoryComponent implements OnInit {
  categoryForm: FormGroup;
  category: INewCategory;
  id: number;

  constructor(public categoryService: CategoriesService,
              private fb: FormBuilder,
              private router: Router,
              private activatedRoute: ActivatedRoute
              ) { }

  ngOnInit(): void {
    this.loadCategory();
    this.updateCategoryForm();
  }

  updateCategoryForm() {
    this.categoryForm = new FormGroup({
     // id: new FormControl(this.category?.id),
      categoryName: new FormControl('', [Validators.required],
      )
    });
  }

  onSubmit() {
    this.categoryService.formData.id = this.category.id;
    this.categoryService.updateCategory(this.categoryForm.value).subscribe(() => {
      this.resetForm(this.categoryForm);
      this.router.navigateByUrl('categories');
    },
    error => {
      console.log(error);
    });
  }

  loadCategory() {
    return this.categoryService.getCategory(+this.activatedRoute.snapshot.paramMap.get('id'))
    .subscribe(response => {
      this.category = response;
     // this.bcService.set('@stockDetails', this.stock.companyName);
    }, error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.categoryService.formData = new INewCategory();
  }

}
