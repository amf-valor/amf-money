import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TradingBooksComponent } from './trading-books/trading-books.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'', component: TradingBooksComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
