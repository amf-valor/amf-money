import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TradingBooksComponent } from './trading-books/trading-books.component';
import { HeaderComponent } from './header/header.component';
import { TradingBookSettingsComponent } from './trading-books/trading-book-settings/trading-book-settings.component';
import { SharedModule } from './shared/shared.module';
import { TradingBookComponent } from './trading-books/trading-book/trading-book.component';
import { HttpClientModule } from '@angular/common/http';
import { AgGridModule } from 'ag-grid-angular';

@NgModule({
  declarations: [
    AppComponent,
    TradingBooksComponent,
    HeaderComponent,
    TradingBookSettingsComponent,
    TradingBookComponent
  ],
  imports: [
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    AgGridModule.withComponents([])
  ],
  entryComponents:[
    TradingBookSettingsComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
