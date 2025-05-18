import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from './components/shared/header/header.component';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import {ToastComponent} from './components/ui/toast/toast.component';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, ToastComponent, NgClass],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  protected static isToastVisible: boolean = false;
  protected static isToastHiding: boolean = false;
  protected static toastMessage: string = 'Успіх!';

  static showMessage(msg?: string, success: boolean = true) {
    if(!this.isToastVisible){
      if(!msg && success){
        msg = 'Успіх!';
      }else if(!msg && !success){
        msg = 'Помилка!';
      }
      this.toastMessage = msg!;
      this.isToastVisible = true;
      setTimeout(()=>{
        this.isToastHiding = true;
        setTimeout(() => {
          this.isToastVisible = false;
          this.toastMessage = 'Успіх!';
          this.isToastHiding = false;
        }, 200);
      }, 2700)
    }
  }

  protected get getIsToastVisible(){
    return AppComponent.isToastVisible;
  }
  protected get getToastMessage(){
    return AppComponent.toastMessage;
  }
  protected get getIsToastHiding(){
    return AppComponent.isToastHiding;
  }
}
