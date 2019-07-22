import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { TradingBook } from '../trading-books/trading-book/trading-book.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Trade } from '../trading-books/trading-book/trade.model';
import { Account } from '../home/sign-up/account.model';
import { TradingBookSettings } from '../trading-books/trading-book-settings/trading-book-settings.model';

@Injectable({
  providedIn: 'root'
})
export class PortalApiService {

  constructor(private httpClient: HttpClient) { }

  createTradingBook(settings: TradingBookSettings) : Observable<number>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.httpClient.post<number>(
      `${environment.PORTAL_API_ADDRESS}/v1/tradingBooks`,
       JSON.stringify(settings),
       {headers: httpHeaders})
  }
  
  getAllTradingBooks(): Observable<TradingBook[]>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.httpClient.get<TradingBook[]>(
      `${environment.PORTAL_API_ADDRESS}/v1/tradingBooks`, 
      {headers: httpHeaders})                    
  }

  updateTrades(tradingBookId, trades: Trade[]): Observable<void>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.httpClient.put(
      `${environment.PORTAL_API_ADDRESS}/v1/tradingBooks/${tradingBookId}/trades`,
       JSON.stringify(trades),
       {headers: httpHeaders}).pipe(map(()=> {return Observable.create()}))
  }

  updateSettings(tradingBookId: number, settings: TradingBookSettings): Observable<void>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.httpClient.put(
      `${environment.PORTAL_API_ADDRESS}/v1/tradingBooks/${tradingBookId}/settings`,
       JSON.stringify(settings),
       {headers: httpHeaders}).pipe(map(()=> {return Observable.create()}))
  }

  post(account: Account): Observable<number>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.httpClient.post<number>(
      `${environment.PORTAL_API_ADDRESS}/v1/accounts`,
       JSON.stringify(account),
       {headers: httpHeaders})
  }

}
