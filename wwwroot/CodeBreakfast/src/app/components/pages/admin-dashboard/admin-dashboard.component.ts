import {Component, OnInit} from '@angular/core';
import {UserService} from '../../../services/user.service';
import {UserCountStatistics} from '../../../models/user.model';
import {firstValueFrom} from 'rxjs';
import {RouterLink} from '@angular/router';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-admin-dashboard',
  imports: [
    RouterLink,
    MatIcon
  ],
  templateUrl: './admin-dashboard.component.html',
  styleUrl: './admin-dashboard.component.scss'
})
export class AdminDashboardComponent implements OnInit {
  protected userCountStatistics: UserCountStatistics | null = null;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserCountStatistics();
  }

  public async loadUserCountStatistics() {
    this.userCountStatistics = (await firstValueFrom(this.userService.getUserCountStatistics())).data;
  }
}
