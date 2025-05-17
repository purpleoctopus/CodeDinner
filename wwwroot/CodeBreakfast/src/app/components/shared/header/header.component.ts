import {Component, OnInit} from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {firstValueFrom, map, Observable} from 'rxjs';
import {LoginFormComponent} from '../../dialogs/login-form/login-form.component';
import {AsyncPipe} from '@angular/common';
import {RegisterFormComponent} from '../../dialogs/register-form/register-form.component';
import {AuthService} from '../../../services/auth.service';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {MatIcon} from '@angular/material/icon';
import {ApiResponse} from '../../../models/response.model';
import {SessionModel} from '../../../models/session.model';

@Component({
  selector: 'app-header',
  imports: [
    MatIcon,
    MatNavList,
    MatListItem,
    RouterLink,
    MatButton,
    AsyncPipe,
    MatMenu,
    MatMenuTrigger,
    MatMenuItem
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  protected isProfileDropdownOpened = false;
  protected userName?: string;

  constructor(private dialog: MatDialog, protected authService: AuthService) {}

  ngOnInit(): void {
    this.authService.sessionData.subscribe(sessionData => {
      this.userName = sessionData?.username
    })
  }

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
