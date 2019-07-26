import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authenticationService.token && this.authenticationService.currentToken) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.authenticationService.token.hash}`
                }
            });
        }

        return next.handle(request);
    }
}