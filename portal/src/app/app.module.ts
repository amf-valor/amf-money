import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TradingBooksComponent } from './trading-books/trading-books.component';
import { HeaderComponent } from './header/header.component';
import { BookSettingsComponent } from './trading-books/book-settings/book-settings.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    TradingBooksComponent,
    HeaderComponent,
    BookSettingsComponent
  ],
  imports: [
    AppRoutingModule,
    SharedModule
  ],
  entryComponents:[
    BookSettingsComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
