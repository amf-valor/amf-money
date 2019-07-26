import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError, of, empty } from 'rxjs';
import { Token } from '../home/login/token.model';
import { environment } from 'src/environments/environment';
import { Credentials } from '../home/login/credentials.model';
import { map, catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {

  private tokenSubject: BehaviorSubject<Token>;
  public currentToken: Observable<Token>;

  constructor(private http: HttpClient) {
      this.tokenSubject = new BehaviorSubject<Token>(JSON.parse(localStorage.getItem(TOKEN_KEY)));
      this.currentToken = this.tokenSubject.asObservable();
  }

  public get token(): Token {
      return this.tokenSubject.value;
  }

  postCredentials(credentials: Credentials): Observable<Token> {
    const httpHeaders = { 'Content-Type': 'application/json'}
    return this.http.post<Token>(`${environment.PORTAL_API_ADDRESS}/v1/accounts/authenticate`, 
    JSON.stringify(credentials),{headers: httpHeaders})
      .pipe(
        map(token => {
          localStorage.setItem(TOKEN_KEY, JSON.stringify(token));
          this.tokenSubject.next(token);
          return token;
        })
      );
  }

  logout() {
    localStorage.removeItem(TOKEN_KEY);
    this.tokenSubject.next(null);
  }
}

const TOKEN_KEY: string = "token";
