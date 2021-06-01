import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { INewCategory } from 'src/app/shared/models/category';
import { CategoriesService } from '../categories.service';


@Component({
  selector: 'app-add-update-category',
  templateUrl: './add-update-category.component.html',
  styleUrls: ['./add-update-category.component.scss']
})
export class AddUpdateCategoryComponent implements OnInit {
  categoryForm: FormGroup;
  id: number;

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private categoryService: CategoriesService,
              private router: Router)
              {
                if (this.route.snapshot.paramMap.get('id')) {
                  this.id = +this.route.snapshot.paramMap.get('id');
                }

                this.categoryForm = this.fb.group({
                  id: 0,
                  categoryName: ['', [Validators.required]],
                });
              }

  ngOnInit(): void {
    if (this.id > 0) {
      this.categoryService.getCategoryById(this.id)
        .subscribe((response: INewCategory) => {
          this.categoryForm.setValue(response);
        }, error => console.error(error));
    }  }

    onSubmit() {

      if (!this.categoryForm.valid) {
        return;
      }
      this.categoryService.updateCategory( this.categoryForm.value );
    }

    cancel() {
      this.router.navigateByUrl('/categories');
    }

    get categoryName() { return this.categoryForm.get('categoryName'); }
}


