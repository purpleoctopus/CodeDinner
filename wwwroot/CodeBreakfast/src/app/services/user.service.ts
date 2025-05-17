import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ApiResponse} from '../models/response.model';
import {UserProfile} from '../models/user.model';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly url: string = `${environment.apiUrl}/user`;

  constructor(private http: HttpClient) { }

  public getMyProfile(){
    return this.http.get<ApiResponse<UserProfile>>(`${this.url}/me`).pipe(
      map(res => {
        res.data!.registeredOn = new Date(res.data!.registeredOn);
        return res;
      })
    );
  }
}
