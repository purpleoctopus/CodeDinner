import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {ApiResponse} from '../models/response.model';
import {AppRole, UserCountStatistics, UserDetail, UserProfile, UserUpdate} from '../models/user.model';
import {map} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly url: string = `${environment.apiUrl}/user`;

  constructor(private http: HttpClient) { }

  public getAllUsers() {
    return this.http.get<ApiResponse<UserDetail[]>>(`${this.url}/all`);
  }

  public getMyProfile(){
    return this.http.get<ApiResponse<UserProfile>>(`${this.url}/me`).pipe(
      map(res => {
        res.data!.registeredOn = new Date(res.data!.registeredOn);
        return res;
      })
    );
  }

  public getUserCountStatistics(){
    return this.http.get<ApiResponse<UserCountStatistics>>(`${this.url}/count-statistics`);
  }

  public updateMyUser(user: UserUpdate){
    return this.http.put<ApiResponse<UserDetail>>(`${this.url}/me`, user)
  }

  public updateUserRole(id:string, role: AppRole) {
    return this.http.put<ApiResponse<boolean>>(`${this.url}/${id}/role`, role)
  }
  public getMyProfilePicture(){
    return this.http.get<ApiResponse<Blob>>(`${this.url}/me/picture`)
  }

  public getUserProfilePicture(id: string){
    return this.http.get<ApiResponse<Blob>>(`${this.url}/${id}/picture`);
  }

  public uploadMyProfilePicture(file: FormData){
    return this.http.post<ApiResponse<boolean>>(`${this.url}/me/picture`, file)
  }

  public getUserProfile(id: string){
    return this.http.get<ApiResponse<UserProfile>>(`${this.url}/${id}`).pipe(
      map(res => {
        res.data!.registeredOn = new Date(res.data!.registeredOn);
        return res;
      })
    );
  }
}
