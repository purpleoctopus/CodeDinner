import {Component, Input} from '@angular/core';
import {MatIcon} from '@angular/material/icon';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [
    MatIcon,
    NgClass
  ],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent {
 @Input() message: string | null = null;
 @Input() success: boolean = true;
}
