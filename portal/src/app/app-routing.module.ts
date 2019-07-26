import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TradingBooksComponent } from './trading-books/trading-books.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'tradingBooks', component: TradingBooksComponent, canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
