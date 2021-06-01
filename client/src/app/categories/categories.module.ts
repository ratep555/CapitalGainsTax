import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoriesComponent } from './categories.component';
import { SharedModule } from '../shared/shared.module';
import { CategoriesRoutingModule } from './categories-routing.module';
import { AddCategoryComponent } from './add-category/add-category.component';
import { Routes } from '@angular/router';
import { UpdateCategoryComponent } from './update-category/update-category.component';
import { AddUpdateCategoryComponent } from './add-update-category/add-update-category.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddEditNewComponent } from './add-edit-new/add-edit-new.component';
import { AddEditNew1Component } from './add-edit-new1/add-edit-new1.component';


@NgModule({
  declarations: [CategoriesComponent,
                 AddCategoryComponent,
                 UpdateCategoryComponent,
                 AddUpdateCategoryComponent,
                 AddEditNewComponent,
                 AddEditNew1Component],
  imports: [
    CommonModule,
    SharedModule,
    CategoriesRoutingModule,
    ReactiveFormsModule
  ]
})
export class CategoriesModule { }
