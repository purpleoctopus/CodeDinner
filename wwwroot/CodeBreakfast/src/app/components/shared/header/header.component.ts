import { Component } from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {firstValueFrom, map, Observable} from 'rxjs';
import {LoginFormComponent} from '../../forms/login-form/login-form.component';
import {AsyncPipe} from '@angular/common';
import {RegisterFormComponent} from '../../forms/register-form/register-form.component';
import {AuthService} from '../../../services/auth.service';

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

  constructor(private dialog: MatDialog, protected authService: AuthService) {}

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected async openRegisterForm(){
    const dialog = this.dialog.open(RegisterFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected logout(){
    this.authService.logout();
  }
}
