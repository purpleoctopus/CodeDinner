import { createAction, props } from '@ngrx/store';
import { User } from './models';

export const login =
  createAction('[Auth] Login', props<{ username: string; password: string }>());
export const loginSuccess =
  createAction('[Auth] Login Success', props<{ accessToken: string; user: User }>());
export const loginFailure =
  createAction('[Auth] Login Failure', props<{ error: string }>());
export const logout =
  createAction('[Auth] Logout');

export const register =
  createAction('[Auth] Register', props<{ username: string; password: string }>());
export const registerSuccess =
  createAction('[Auth] Register Success', props<{ accessToken: string; user: User }>());
export const registerFailure =
  createAction('[Auth] Register Failure', props<{ error: string }>());
