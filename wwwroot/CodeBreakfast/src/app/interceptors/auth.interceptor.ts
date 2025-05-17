import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, switchMap, take, throwError} from 'rxjs';
import {AuthService} from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const service = inject(AuthService);

  return service.sessionData.pipe(
    take(1),
    switchMap(sessionData => {
      if (!sessionData && !req.url.includes('/auth')) {
        return throwError(() => new Error('No access token available'));
      }

      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${sessionData?.accessToken}`
        }
      });

      return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            service.logout();
          }
          return throwError(() => error);
        })
      );
    })
  );
};
