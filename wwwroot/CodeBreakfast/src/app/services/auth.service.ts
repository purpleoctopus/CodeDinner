import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Course, CourseAddDto, CourseUpdateDto} from '../models/course.model';
import {BehaviorSubject, firstValueFrom, map, Observable} from 'rxjs';
import {LoginDto, RegisterDto} from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly url: string = `${environment.apiUrl}/Auth`;
  private accessToken$ : BehaviorSubject<string | null> =
    new BehaviorSubject<string | null>(localStorage.getItem('accessToken'));
  public accessToken = this.accessToken$.asObservable();

  constructor(private http: HttpClient) {
    this.accessToken$.subscribe(token => {
      if (token) {
        localStorage.setItem('accessToken', token)
      }else{
        localStorage.removeItem('accessToken');
      }
    })
  }

  public async login(data: LoginDto){
    const res = this.http.post<any>(`${this.url}/Login`, data);
    this.accessToken$.next((await firstValueFrom(res)).data.accessToken);
    return res;
  }

  public register(data: RegisterDto){
    return this.http.post<any>(`${this.url}/Register`, data);
  }

  public logout(){
    this.accessToken$.next(null);
  }
}
