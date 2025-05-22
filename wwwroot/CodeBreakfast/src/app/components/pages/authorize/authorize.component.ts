import { Component } from '@angular/core';
import {LoginFormComponent} from '../../dialogs/login-form/login-form.component';
import {firstValueFrom} from 'rxjs';
import {RegisterFormComponent} from '../../dialogs/register-form/register-form.component';
import {MatDialog} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';

@Component({
  selector: 'app-authorize',
  imports: [
    MatButton,
  ],
  templateUrl: './authorize.component.html',
  styleUrl: './authorize.component.scss'
})
export class AuthorizeComponent {
  constructor(public dialog: MatDialog) {}

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected async openRegisterForm(){
    const dialog = this.dialog.open(RegisterFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }
}
