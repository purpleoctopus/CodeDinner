import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HeaderComponent} from './components/shared/header/header.component';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import {ToastComponent} from './components/ui/toast/toast.component';
import {AsyncPipe, NgClass} from '@angular/common';
import {LoadingComponent} from './components/shared/loading/loading.component';
import {BehaviorSubject, firstValueFrom, Observable, skip, switchMap} from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, ToastComponent, NgClass, LoadingComponent, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent{
  private static loadingRequest$ = new BehaviorSubject<Promise<any> | null>(null);
  protected static isToastVisible: boolean = false;
  protected static isToastHiding: boolean = false;
  protected static toastMessage: string = 'Успіх!';
  protected static isShowLoading = new BehaviorSubject<boolean>(false);

  constructor() {
    AppComponent.loadingRequest$.pipe(switchMap(promise => {
        if (!promise) return [];
        queueMicrotask(() => AppComponent.isShowLoading.next(true));
        return promise.then(
          res => res,
          err => { throw err; }
        ).finally(() => AppComponent.isShowLoading.next(false));
      })
    ).subscribe();
  }

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
          this.isToastHiding = false;
        }, 200);
      }, 2700)
    }
  }

  static showLoadingFromPromise(promise: Promise<any>) {
    this.loadingRequest$.next(promise);
    return promise;
  }
  static showLoadingFromObservable(observable: Observable<any>) {
    const promise = firstValueFrom(observable);
    this.loadingRequest$.next(promise);
    return observable;
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
  protected get getIsShowLoading(){
    return AppComponent.isShowLoading;
  }
}
