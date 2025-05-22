import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {inject} from '@angular/core';
import {catchError, switchMap, take, throwError} from 'rxjs';
import {AuthService} from '../services/auth.service';
import {Router} from '@angular/router';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const service = inject(AuthService);
  const router = inject(Router);

  return service.sessionData.pipe(
    take(1),
    switchMap(sessionData => {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${sessionData?.accessToken}`
        }
      });

      return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            if(sessionData) {
              service.logout();
            }else{
              router.navigate(['/authorize']);
            }
            return throwError(() => new Error('No access token available'));
          }else if(error.status === 403){
            if(sessionData) {
              router.navigate(['/no-access']);
            }
            return throwError(() => new Error('No access.'));
          }
          return throwError(() => error);
        })
      );
    })
  );
};
