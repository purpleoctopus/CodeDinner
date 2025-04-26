import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, EMPTY, filter, of, switchMap, take} from 'rxjs';
import {AuthService} from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const service = inject(AuthService);

  return service.accessToken.pipe(
    filter(token => !!token || req.url.includes('/Login')),
    take(1),
    switchMap(token => {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next(req).pipe(catchError((error : HttpErrorResponse) => {
        if(error.status === 401) {
          service.logout();
        }
        return of();
      }))
    })
  );
};
