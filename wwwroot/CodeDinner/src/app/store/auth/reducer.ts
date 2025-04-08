import { createReducer, on } from '@ngrx/store';
import * as AuthActions from './actions';
import { AuthState } from './models';

export const initialState: AuthState = {
  accessToken: null,
  user: null,
  loading: false,
  error: null,
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.login, state => ({ ...state, loading: true, error: null })),
  on(AuthActions.loginSuccess, (state, { accessToken, user }) => ({
    ...state,
    accessToken,
    user,
    loading: false
  })),
  on(AuthActions.loginFailure, (state, { error }) => ({
    ...state,
    error,
    loading: false
  })),
  on(AuthActions.logout, () => initialState)
);
