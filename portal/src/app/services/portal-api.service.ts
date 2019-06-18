import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TradingBook } from '../trading-books/trading-book/trading-book.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PortalApiService {

  constructor(private httpClient: HttpClient) { }

  createTradingBook(settings: TradingBook) : Observable<TradingBook>{
    const httpHeaders = { 'Content-Type': 'application/json'}

    return this.httpClient.post<TradingBook>(
      `${environment.PORTAL_API_ADDRESS}/tradingBooks`,
       JSON.stringify(settings),
       {headers: httpHeaders})
  }
  
  getAllTradingBooks(): Observable<TradingBook[]>{
    const httpHeaders = { 'Content-Type': 'application/json'}
    
    return this.httpClient.get<TradingBook[]>(
      `${environment.PORTAL_API_ADDRESS}/tradingBooks`, 
      {headers: httpHeaders})                    
  }
}
