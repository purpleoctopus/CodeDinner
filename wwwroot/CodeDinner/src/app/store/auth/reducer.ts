import { createReducer, on } from '@ngrx/store';
import * as AuthActions from './actions';
import { AuthState } from './models';

export const initialState: AuthState = {
  accessToken: localStorage.getItem('accessToken'),
  error: null,
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.login, state => ({ ...state, loading: true, error: null })),
  on(AuthActions.loginSuccess, (state, { accessToken }) => {
    localStorage.setItem('accessToken', accessToken);
    return {...state, accessToken};
  }),
  on(AuthActions.loginFailure, (state, { error }) => {
    return {...state, error}
  }),
  on(AuthActions.logout, (state) => {
    localStorage.removeItem('accessToken');
    return {...state, accessToken: null};
  })
);
