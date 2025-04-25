import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from './components/shared/header/header.component';
import {provideStore} from '@ngrx/store';
import {AuthEffects} from './store/auth/effects';
import {authReducer} from './store/auth/reducer';
import {HTTP_INTERCEPTORS} from '@angular/common/http';
import {authInterceptor} from './interceptors/auth.interceptor';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

}
