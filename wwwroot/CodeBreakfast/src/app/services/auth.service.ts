import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, firstValueFrom} from 'rxjs';
import {LoginDto, RegisterDto} from '../models/auth.model';
import {ApiResponse} from '../models/response.model';
import {SessionModel} from '../models/session.model';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly url: string = `${environment.apiUrl}/auth`;
  private sessionData$ : BehaviorSubject<SessionModel | null> =
    new BehaviorSubject<SessionModel | null>(JSON.parse(localStorage.getItem('sessionData') as string));
  public sessionData = this.sessionData$.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.sessionData$.subscribe(sessionModel => {
      if (sessionModel) {
        localStorage.setItem('sessionData', JSON.stringify(sessionModel))
      }else{
        localStorage.removeItem('sessionData');
      }
    })
  }

  public async login(data: LoginDto): Promise<ApiResponse<SessionModel>>{
    const res = await firstValueFrom(this.http.post<ApiResponse<SessionModel>>(`${this.url}/login`, data));
    this.sessionData$.next(res.data);
    if(res.success){
      if(this.router.url.includes('authorize')){
        this.router.navigate(['/']);
      }
    }
    return res;
  }

  public register(data: RegisterDto){
    return this.http.post<any>(`${this.url}/register`, data);
  }

  public logout(){
    this.sessionData$.next(null);
    this.router.navigate(['/authorize']);
  }
}
