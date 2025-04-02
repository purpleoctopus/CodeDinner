import { Component } from '@angular/core';
import {MatListItem, MatNavList} from '@angular/material/list';
import {RouterLink} from '@angular/router';
import {MatButton} from '@angular/material/button';

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

}
