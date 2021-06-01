import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ICategory } from 'src/app/shared/models/category';
import { CategoriesService } from '../categories.service';

@Component({
  selector: 'app-add-edit-new',
  templateUrl: './add-edit-new.component.html',
  styleUrls: ['./add-edit-new.component.scss']
})
export class AddEditNewComponent implements OnInit {
  id: number;
  category: ICategory;
  editForm;

  constructor(public categoryService: CategoriesService,
              private activatedRoute: ActivatedRoute,
              private router: Router,
              private formBuilder: FormBuilder)
              {
                this.editForm = this.formBuilder.group({
                  id: [''],
                  categoryName: ['', Validators.required],
                });
              }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.params['playerId'];
    this.loadCategory();

    this.categoryService.getCategory2(this.id).subscribe((data: ICategory) => {
      this.category = data;
      this.editForm.patchValue(data);
    });
  }

  onSubmit(formData) {
    this.categoryService.formData.id = this.category.id;
    this.categoryService.updateCategory2(this.id, formData.value).subscribe(res => {
      this.router.navigateByUrl('categories');
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

}
