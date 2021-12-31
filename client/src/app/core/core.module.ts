import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { SectionHeaderComponent } from './section-header/section-header.component';
import { BreadcrumbModule } from 'xng-breadcrumb';
import { SharedModule } from '../shared/shared.module';
import { NavBar1Component } from './nav-bar1/nav-bar1.component';
import { NavBar2Component } from './nav-bar2/nav-bar2.component';


@NgModule({
  declarations: [NavBarComponent, SectionHeaderComponent, NavBar1Component, NavBar2Component],
  imports: [
    CommonModule,
    RouterModule,
    BreadcrumbModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    }),
    CollapseModule.forRoot()
  ],
  exports: [
    NavBarComponent,
    NavBar1Component,
    NavBar2Component,
    SectionHeaderComponent
  ]
})
export class CoreModule { }
