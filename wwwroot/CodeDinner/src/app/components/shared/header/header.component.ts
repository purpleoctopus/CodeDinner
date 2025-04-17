import { Component } from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {AddCourseFormComponent} from '../../features/add-course-form/add-course-form.component';
import {firstValueFrom, map, Observable} from 'rxjs';
import {LoginFormComponent} from '../../features/login-form/login-form.component';
import {AuthService} from '../../../services/auth.service';
import {State, Store} from '@ngrx/store';
import {AsyncPipe} from '@angular/common';
import {logout} from '../../../store/auth/actions';

@Component({
  selector: 'app-header',
  imports: [
    MatNavList,
    MatListItem,
    RouterLink,
    MatButton,
    AsyncPipe
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  protected accessToken$: Observable<string> | null = null;

  constructor(private dialog: MatDialog, state: State<object>, private store: Store) {
    this.accessToken$ = state.pipe(map(state => state.auth.accessToken));
  }

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected logout(){
    this.store.dispatch(logout())
  }
}
