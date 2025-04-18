import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './models';

export const selectAuth = createFeatureSelector<AuthState>('auth');

export const selectAccessToken = createSelector(selectAuth, s => s.accessToken);
