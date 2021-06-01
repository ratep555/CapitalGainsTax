import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AddCategoryComponent } from './add-category/add-category.component';
import { CategoriesComponent } from './categories.component';
import { UpdateCategoryComponent } from './update-category/update-category.component';
import { AddUpdateCategoryComponent } from './add-update-category/add-update-category.component';
import { AddEditNewComponent } from './add-edit-new/add-edit-new.component';
import { AddEditNew1Component } from './add-edit-new1/add-edit-new1.component';


const routes: Routes = [
  {path: '', component: CategoriesComponent},
  {path: 'addcategory', component: AddCategoryComponent, data: {breadcrumb: 'Add Category'}},
  {path: 'updatecategory/:id', component: UpdateCategoryComponent, data: {breadcrumb: 'Update Category'}},
  {path: 'updatecategory1/:id', component: AddUpdateCategoryComponent, data: {breadcrumb: 'Update Category'}},
  {path: 'updatecategory2/:id', component: AddEditNewComponent, data: {breadcrumb: 'Update Category'}},
  {path: 'updatecategory3/:id', component: AddEditNew1Component, data: {breadcrumb: 'Update Category'}}
];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }






