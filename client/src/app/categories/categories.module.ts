import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories.component';
import { SharedModule } from '../shared/shared.module';
import { CategoriesRoutingModule } from './categories-routing.module';
import { AddCategoryComponent } from './add-category/add-category.component';



@NgModule({
  declarations: [CategoriesComponent, AddCategoryComponent],
  imports: [
    CommonModule,
    SharedModule,
    CategoriesRoutingModule
  ]
})
export class CategoriesModule { }
