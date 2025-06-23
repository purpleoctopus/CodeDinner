import { Component } from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {RouterLink} from "@angular/router";
import {AppRole} from '../../../models/user.model';
import {AuthService} from '../../../services/auth.service';
import {LoginFormComponent} from '../../dialogs/login-form/login-form.component';
import {firstValueFrom} from 'rxjs';
import {MatDialog} from '@angular/material/dialog';
import {RegisterFormComponent} from '../../dialogs/register-form/register-form.component';
import {MatButton} from '@angular/material/button';
import {MatMenuItem} from '@angular/material/menu';

@Component({
  selector: 'app-burger-menu',
  imports: [
    AsyncPipe,
    RouterLink,
    MatButton,
    MatMenuItem
  ],
  templateUrl: './burger-menu.component.html',
  styleUrl: './burger-menu.component.scss'
})
export class BurgerMenuComponent {
  constructor(private dialog: MatDialog, protected authService: AuthService) {
  }

  protected async openRegisterForm(){
    const dialog = this.dialog.open(RegisterFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected logout(){
    this.authService.logout();
  }

  protected readonly AppRole = AppRole;
}
