import { Component } from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';
import {MatDialog} from '@angular/material/dialog';
import {AddCourseFormComponent} from '../../features/add-course-form/add-course-form.component';
import {firstValueFrom} from 'rxjs';
import {LoginFormComponent} from '../../features/login-form/login-form.component';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-header',
  imports: [
    MatNavList,
    MatListItem,
    RouterLink,
    MatButton
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private dialog: MatDialog, private authService: AuthService) {}

  protected async openLoginForm(){
    const dialog = this.dialog.open(LoginFormComponent);
    const dialogResult = await firstValueFrom(dialog.afterClosed());
    if(dialogResult){
      const res = await this.authService.login(dialogResult);
    }
  }
}
