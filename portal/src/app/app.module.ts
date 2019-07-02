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
import { NumericCellEditorComponent } from './trading-books/numeric-cell-editor/numeric-cell-editor.component';
import { MoneyCellRendererComponent } from './trading-books/trading-book/money-cell-renderer/money-cell-renderer.component';
import { PercentCellRendererComponent } from './trading-books/trading-book/percent-cell-renderer/percent-cell-renderer.component';
import { FooterComponent } from './footer/footer.component';
import { SignUpComponent } from './home/sign-up/sign-up.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './home/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    TradingBooksComponent,
    HeaderComponent,
    TradingBookSettingsComponent,
    TradingBookComponent,
    NumericCellEditorComponent,
    MoneyCellRendererComponent,
    PercentCellRendererComponent,
    FooterComponent,
    SignUpComponent,
    HomeComponent,
    LoginComponent,
  ],
  imports: [
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    AgGridModule.withComponents([
      NumericCellEditorComponent, 
      MoneyCellRendererComponent,
      PercentCellRendererComponent
    ])
  ],
  entryComponents:[
    TradingBookSettingsComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
