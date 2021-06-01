import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { StocksModule } from './stocks/stocks.module';
import { HomeModule } from './home/home.module';
import { TransactionsModule } from './transactions/transactions.module';
import { MyportfolioModule } from './myportfolio/myportfolio.module';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { FormsModule } from '@angular/forms';
import { TaxliabilityModule } from './taxliability/taxliability.module';
import { CategoriesModule } from './categories/categories.module';
import { CountriesModule } from './countries/countries.module';
import { StocksAdminModule } from './stocks-admin/stocks-admin.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    HomeModule,
    TransactionsModule,
    MyportfolioModule,
    TaxliabilityModule,
    CountriesModule,
    StocksAdminModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
