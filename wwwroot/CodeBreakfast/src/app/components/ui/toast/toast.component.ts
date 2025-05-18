import {Component, Input} from '@angular/core';
import {MatIcon} from '@angular/material/icon';

@Component({
  selector: 'app-toast',
  imports: [
    MatIcon
  ],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.scss'
})
export class ToastComponent {
 @Input() message: string | null = null;
}
