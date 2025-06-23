import {Component, OnInit} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {firstValueFrom} from 'rxjs';
import {AppRole, UserDetail, UserDetailWithPicture} from '../../../models/user.model';
import {AppComponent} from '../../../app.component';
import {AsyncPipe, NgClass} from '@angular/common';
import {AuthService} from '../../../services/auth.service';
import {MatDialog} from '@angular/material/dialog';
import {ChangeRoleComponent} from '../../dialogs/change-role/change-role.component';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-users-list',
  imports: [
    NgClass,
    AsyncPipe,
    RouterLink
  ],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.scss'
})
export class UsersListComponent implements OnInit {
  protected users: UserDetailWithPicture[] = []

  constructor(private userService: UserService, protected authService: AuthService,
      private dialog: MatDialog) { }

  ngOnInit() {
    this.getUsersList()
  }

  public async getUsersList(){
    const res = (await AppComponent.showLoadingFromPromise(firstValueFrom(this.userService.getAllUsers()))).data;
    this.users = res ?? [];
    this.users.map(async (user: UserDetailWithPicture) => {
      const response = (await firstValueFrom(this.userService.getUserProfilePicture(user.id))).data;
      if(response) {
        user.picture = 'data:image/jpeg;base64,' + response;
      }
    })
  }

  public async showChangeRoleModal(user: { id: string, name: string }){
    const dialog = this.dialog.open(ChangeRoleComponent, {
      data: user
    });
    await firstValueFrom(dialog.afterClosed());
    await this.getUsersList();
  }

  protected readonly AppRole = AppRole;
}
