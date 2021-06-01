import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INewCategory } from 'src/app/shared/models/category';
import { CategoriesService } from '../categories.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent implements OnInit {
  categoryForm: FormGroup;

  constructor(private categoryService: CategoriesService, private router: Router) { }

  ngOnInit(): void {
    this.createCategoryForm();
  }

  createCategoryForm() {
    this.categoryForm = new FormGroup({
      categoryName: new FormControl('', [Validators.required, Validators.minLength(1)])
    });
  }

  get catName() {
    return this.categoryForm.get('categoryName');
}

  onSubmit() {
    this.categoryService.createCategory(this.categoryForm.value).subscribe(() => {
      this.resetForm(this.categoryForm);
      this.router.navigateByUrl('categories');
    },
    error => {
      console.log(error);
    });
  }

  resetForm(form: FormGroup) {
    form.reset();
    this.categoryService.formData = new INewCategory();
  }
}
