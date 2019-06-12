import { Injectable } from '@angular/core';
import { TradingBookSettings } from '../trading-books/trading-book-settings/trading-book-settings.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TradingBook } from '../trading-books/trading-book/trading-book.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PortalApiService {

  constructor(private httpClient: HttpClient) { }

  createTradingBook(tradingBookSettings: TradingBookSettings) : Observable<TradingBook>{
    const httpHeaders = { 'Content-Type': 'application/json'}

    return this.httpClient.post<TradingBookSettings>(`${environment.PORTAL_API_ADDRESS}/tradingBooks`,
                                JSON.stringify(tradingBookSettings),
                                {headers: httpHeaders})
                          .pipe(
                            map(res => {
                              const newTradingBook:TradingBook = {
                                settings: res
                              }
                              return newTradingBook
                            })
                          )
  }
}
