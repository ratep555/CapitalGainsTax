import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { CategoriesService } from '../categories.service';

@Component({
  selector: 'app-add-edit-new1',
  templateUrl: './add-edit-new1.component.html',
  styleUrls: ['./add-edit-new1.component.scss']
})
export class AddEditNew1Component implements OnInit {
  form: FormGroup;
  id: number;
  isAddModel: boolean;
  loading: boolean;
  submitted: boolean;

  constructor(private formBuilder: FormBuilder,
              private route: ActivatedRoute,
              private router: Router,
              private categoryService: CategoriesService
             ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];

    this.form = this.formBuilder.group({
      id: [this.id],
      categoryName: ['', Validators.required],
    });

    this.categoryService.getById(this.id)
    .pipe(first())
    .subscribe(x => this.form.patchValue(x));
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    this.updateUser();

}

private updateUser() {
  this.categoryService.updateCategory3(this.id, this.form.value)
      .pipe(first())
      .subscribe(() => {
          this.router.navigateByUrl('categories');
        }, error => {
          console.log(error);
        });
      }
}










