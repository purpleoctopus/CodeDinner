import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as AuthActions from './actions';
import { AuthService } from '../../services/auth.service';
import {catchError, map, Observable, of, switchMap} from 'rxjs';

@Injectable()
export class AuthEffects {
  login$: Observable<any> | null = null;

  constructor(private actions$: Actions, private authService: AuthService) {
    this.login$ = createEffect(() =>
      this.actions$.pipe(
        ofType(AuthActions.login),
        switchMap(({ username: username, password }) =>
          this.authService.login({username, password}).pipe(
            map(response => AuthActions.loginSuccess({ accessToken: response.accessToken, user: response.user })),
            catchError(error => of(AuthActions.loginFailure({ error: error.message || 'Login failed' })))
          )
        )
      )
    );
  }
}
