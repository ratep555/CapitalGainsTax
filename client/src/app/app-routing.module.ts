import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { CategoriesComponent } from './categories/categories.component';
import { AuthGuard } from './core/guards/auth.guard';
import { CountriesComponent } from './countries/countries.component';
import { HomeComponent } from './home/home.component';
import { MyportfolioComponent } from './myportfolio/myportfolio.component';
import { StocksAdminComponent } from './stocks-admin/stocks-admin.component';
import { TaxliabilityComponent } from './taxliability/taxliability.component';
import { TransactionsComponent } from './transactions/transactions.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'stocks', canActivate: [AuthGuard],
  loadChildren: () => import('./stocks/stocks.module').then(mod => mod.StocksModule),
  data: {breadcrumb: 'List of Stocks'}},
  {path: 'transactions', canActivate: [AuthGuard],
  loadChildren: () => import('./transactions/transactions.module').then(mod => mod.TransactionsModule),
  data: {breadcrumb: 'List of Transactions'}},
  {path: 'myportfolio', canActivate: [AuthGuard],
  component: MyportfolioComponent, data: {breadcrumb: 'My Portfolio'}},
  {path: 'taxliability', canActivate: [AuthGuard], component: TaxliabilityComponent,
  data: {breadcrumb: 'Tax Liability'}},
  // {path: 'categories', component: CategoriesComponent, data: {breadcrumb: 'Categories'}},
  {path: 'categories', canActivate: [AuthGuard],
  loadChildren: () => import('./categories/categories.module').then(mod => mod.CategoriesModule),
  data: {breadcrumb: 'List of Categories'}},
  // {path: 'countries', component: CountriesComponent, data: {breadcrumb: 'Countries'}},
  {path: 'countries', canActivate: [AuthGuard],
  loadChildren: () => import('./countries/countries.module').then(mod => mod.CountriesModule),
  data: {breadcrumb: 'List of Countries'}},
  // {path: 'stocksAdmin', component: StocksAdminComponent, data: {breadcrumb: 'List of Stocks'}},
  {path: 'stocksAdmin', canActivate: [AuthGuard],
  loadChildren: () => import('./stocks-admin/stocks-admin.module').then(mod => mod.StocksAdminModule),
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
