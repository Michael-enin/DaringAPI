import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { NEVER, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';

@Injectable()
// I need to provide this interceptor in app.module.ts 

export class JwtInterceptor implements HttpInterceptor {

  constructor(private accounService:AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let _currentUser!:User;
    this.accounService.currentUser$.pipe(take(1)).subscribe(userFromResponse=>                                                            _currentUser=userFromResponse);
      if(_currentUser){
            request=request.clone({
                 setHeaders:{
                   Authorization:`Bearer ${_currentUser.token}`
                 }
        });
      }
    return next.handle(request);
  } 
}
