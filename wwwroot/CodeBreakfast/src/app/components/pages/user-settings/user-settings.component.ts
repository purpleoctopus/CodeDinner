import {Component, OnInit} from '@angular/core';
import {MatList, MatListItem} from '@angular/material/list';
import {MatButton, MatIconButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {UserSettingsGeneralComponent} from '../../partials/user-settings-general/user-settings-general.component';
import {AsyncPipe, NgClass} from '@angular/common';
import {UserSettingsPrivacyComponent} from '../../partials/user-settings-privacy/user-settings-privacy.component';
import {UserDetail, UserUpdate} from '../../../models/user.model';
import {BehaviorSubject, firstValueFrom, Subject} from 'rxjs';
import {UserService} from '../../../services/user.service';
import {AppComponent} from '../../../app.component';

enum Tab {
  General= 1,
  Privacy = 2,
}

@Component({
  selector: 'app-user-settings',
  imports: [
    MatList,
    MatIcon,
    MatButton,
    UserSettingsGeneralComponent,
    NgClass,
    UserSettingsPrivacyComponent,
    AsyncPipe
  ],
  templateUrl: './user-settings.component.html',
  styleUrl: './user-settings.component.scss'
})

export class UserSettingsComponent implements OnInit {
  private user: BehaviorSubject<UserDetail | null> = new BehaviorSubject<UserDetail | null>(null);
  public user$ = this.user.asObservable();
  protected openedTab: Tab = Tab.General;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUser()
  }

  private async loadUser(){
    const userProfile = (await AppComponent.showLoadingFromPromise(firstValueFrom(this.userService.getMyProfile()))).data;
    if(userProfile) {
      this.user.next({id: userProfile.id, username: userProfile.username, firstName: userProfile.firstName, lastName: userProfile.lastName});
    }
  }

  public openTab(tab: Tab) {
    this.openedTab = tab;
  }

  protected readonly Tab = Tab;
}
