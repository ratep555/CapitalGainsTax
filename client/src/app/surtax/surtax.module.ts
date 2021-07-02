import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SurtaxComponent } from './surtax.component';
import { AddSurtaxComponent } from './add-surtax/add-surtax.component';
import { EditSurtaxComponent } from './edit-surtax/edit-surtax.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { SurtaxRoutingModule } from './surtax-routing.module';



@NgModule({
  declarations: [SurtaxComponent, AddSurtaxComponent, EditSurtaxComponent],
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    SurtaxRoutingModule
  ]
})
export class SurtaxModule { }
