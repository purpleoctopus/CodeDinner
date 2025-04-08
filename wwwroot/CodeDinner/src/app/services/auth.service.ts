import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Course, CourseAddDto, CourseUpdateDto} from '../models/course.model';
import {firstValueFrom} from 'rxjs';
import {LoginDto, RegisterDto} from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly url: string = `${environment.apiUrl}/Auth`;

  constructor(private http: HttpClient) { }

  public login(data: LoginDto){
    return this.http.post<any>(`${this.url}/Login`, data);
  }

  public register(data: RegisterDto){
    return this.http.post<any>(`${this.url}/Register`, data);
  }
}
