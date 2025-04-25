import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as AuthActions from './actions';
import { AuthService } from '../../services/auth.service';
import {catchError, map, Observable, of, switchMap} from 'rxjs';

@Injectable()
export class AuthEffects {
  login$: Observable<any> | null = null;
  register$: Observable<any> | null = null;

  constructor(private actions$: Actions, private authService: AuthService) {
    this.login$ = createEffect(() =>
      this.actions$.pipe(
        ofType(AuthActions.login),
        switchMap(({ username: username, password }) =>
          this.authService.login({username, password}).pipe(
            map(response => AuthActions.loginSuccess({ accessToken: response.data.accessToken, user: response.data.user })),
            catchError(error => of(AuthActions.loginFailure({ error: error.message || 'Login failed' })))
          )
        )
      )
    );
    this.register$ = createEffect(() =>
      this.actions$.pipe(
        ofType(AuthActions.register),
        switchMap(({ username: username, password }) =>
          this.authService.register({username, password}).pipe(
            map(response => AuthActions.registerSuccess({ accessToken: response.data.accessToken, user: response.data.user })),
            catchError(error => of(AuthActions.registerFailure({ error: error.message || 'Register failed' })))
          )
        )
      )
    );
  }
}
