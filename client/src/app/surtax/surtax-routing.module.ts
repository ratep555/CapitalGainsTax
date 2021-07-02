import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SurtaxComponent } from './surtax.component';
import { AddSurtaxComponent } from './add-surtax/add-surtax.component';
import { EditSurtaxComponent } from './edit-surtax/edit-surtax.component';

const routes: Routes = [
  {path: '', component: SurtaxComponent},
  {path: 'addsurtax', component: AddSurtaxComponent, data: {breadcrumb: 'Add Surtax'}},
  {path: 'editsurtax/:id', component: EditSurtaxComponent, data: {breadcrumb: 'Update Surtax'}}

];


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]})

export class SurtaxRoutingModule { }







