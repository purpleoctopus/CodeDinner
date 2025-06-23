import { Component } from '@angular/core';
import {MatIconButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-author-dashboard',
  imports: [
    MatIconButton,
    MatIcon,
    RouterLink
  ],
  templateUrl: './author-dashboard.component.html',
  styleUrl: './author-dashboard.component.scss'
})
export class AuthorDashboardComponent {

}
