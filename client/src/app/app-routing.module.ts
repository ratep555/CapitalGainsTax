import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'stocks', canActivate: [AuthGuard],
  loadChildren: () => import('./stocks/stocks.module').then(mod => mod.StocksModule),
  data: {breadcrumb: 'List of Stocks'}},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
  data: {breadcrumb: {skip: true}}},
  {path: '**', redirectTo: '', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
