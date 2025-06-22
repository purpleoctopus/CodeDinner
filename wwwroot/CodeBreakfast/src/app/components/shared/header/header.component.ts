import {Component, OnInit} from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {firstValueFrom} from 'rxjs';
import {LoginFormComponent} from '../../dialogs/login-form/login-form.component';
import {AsyncPipe, NgClass} from '@angular/common';
import {RegisterFormComponent} from '../../dialogs/register-form/register-form.component';
import {AuthService} from '../../../services/auth.service';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {MatIcon} from '@angular/material/icon';
import {UserService} from '../../../services/user.service';
import {AppRole} from '../../../models/user.model';
import {AppComponent} from '../../../app.component';

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
    MatMenuItem,
    NgClass
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  protected isProfileDropdownOpened = false;
  protected userName?: string;

  constructor(private dialog: MatDialog, protected authService: AuthService, private userService: UserService) {}

  ngOnInit(): void {
    this.authService.sessionData.subscribe(() => {
      this.setUserName()
    })
  }

  private async setUserName(){
    const user = (await firstValueFrom(this.userService.getMyProfile())).data;
    if(user){
      this.userName = user.username;
    }
  }

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected async openRegisterForm(){
    const dialog = this.dialog.open(RegisterFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
  }

  protected openBurgerMenu(): void {
    AppComponent.toggleBurgerMenu();
  }

  protected logout(){
    this.authService.logout();
  }

  protected readonly AppRole = AppRole;
  protected readonly AppComponent = AppComponent;
  protected readonly console = console;
}
