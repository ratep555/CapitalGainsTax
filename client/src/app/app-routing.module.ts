import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { StockDetailsComponent } from './stocks/stock-details/stock-details.component';
import { StocksComponent } from './stocks/stocks.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'stocks', loadChildren: () => import('./stocks/stocks.module').then(mod => mod.StocksModule) },
  {path: '**', redirectTo: '', pathMatch: 'full'}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
