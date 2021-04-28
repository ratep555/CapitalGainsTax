import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './core/guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { MyportfolioComponent } from './myportfolio/myportfolio.component';
import { TransactionsComponent } from './transactions/transactions.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: 'Home'}},
  {path: 'stocks', canActivate: [AuthGuard],
  loadChildren: () => import('./stocks/stocks.module').then(mod => mod.StocksModule),
  data: {breadcrumb: 'List of Stocks'}},
  {path: 'transactions', canActivate: [AuthGuard],
  loadChildren: () => import('./transactions/transactions.module').then(mod => mod.TransactionsModule),
  data: {breadcrumb: 'List of Transactions'}},
  {path: 'myportfolio', canActivate: [AuthGuard], component: MyportfolioComponent,
  data: {breadcrumb: 'My Portfolio'}},
  {path: 'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule),
  data: {breadcrumb: {skip: true}}},
  {path: '**', redirectTo: '', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
