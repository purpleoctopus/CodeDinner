import {Component, OnInit} from '@angular/core';
import {NavigationEnd, Router, RouterOutlet} from '@angular/router';
import {HeaderComponent} from './components/shared/header/header.component';
import {ToastComponent} from './components/ui/toast/toast.component';
import {AsyncPipe, NgClass} from '@angular/common';
import {LoadingComponent} from './components/shared/loading/loading.component';
import {BehaviorSubject, filter, firstValueFrom, Observable, switchMap} from 'rxjs';
import {BurgerMenuComponent} from './components/shared/burger-menu/burger-menu.component';
import {AppRole} from './models/user.model';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, ToastComponent, NgClass, LoadingComponent, AsyncPipe, BurgerMenuComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private static loadingRequest$ = new BehaviorSubject<Promise<any> | null>(null);
  protected static isToastVisible: boolean = false;
  protected static isToastHiding: boolean = false;
  protected static isToastSuccess: boolean = true;
  protected static toastMessage: string = 'Успіх!';
  protected static isShowLoading = new BehaviorSubject<boolean>(false);
  protected static isShowBurgerMenu: boolean = false;

  constructor(private router: Router) {
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

  ngOnInit(): void {
    this.router.events.pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((event) => {
        AppComponent.isShowBurgerMenu = false;
      });
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
      this.isToastSuccess = success;
      setTimeout(()=>{
        this.isToastHiding = true;
        setTimeout(() => {
          this.isToastVisible = false;
          this.isToastHiding = false;
        }, 200);
      }, 2700)
    }
  }

  static toggleBurgerMenu() {
    this.isShowBurgerMenu = !this.isShowBurgerMenu;
  }

  static showLoadingFromPromise<T>(promise: Promise<T>) {
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
  protected get getIsToastSuccess(){
    return AppComponent.isToastSuccess;
  }
  protected get getIsShowLoading(){
    return AppComponent.isShowLoading;
  }
  protected get getIsShowBurgerMenu(){
    return AppComponent.isShowBurgerMenu;
  }
  public static get getIsShowBurgerMenu(){
    return AppComponent.isShowBurgerMenu;
  }
}
