import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TradingBookComponent } from './trading-book/trading-book.component';

const routes: Routes = [{path:'', component: TradingBookComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
