import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import {HTTP_INTERCEPTORS, provideHttpClient} from '@angular/common/http';
import {provideStore} from '@ngrx/store';
import {authReducer} from './store/auth/reducer';
import {provideEffects} from '@ngrx/effects';
import {AuthEffects} from './store/auth/effects';
import {authInterceptor} from './interceptors/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideStore({
      auth: authReducer
    }),
    provideEffects(
      AuthEffects
    ),
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: authInterceptor,
      multi: true
    }
  ]
};
