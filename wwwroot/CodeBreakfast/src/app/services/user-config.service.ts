import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {ApiResponse} from '../models/response.model';
import {UserConfigDetailDto, UserConfigUpdateDto} from '../models/user-config.model';

@Injectable({
  providedIn: 'root'
})
export class UserConfigService {
  private readonly url: string = `${environment.apiUrl}/user/me/settings`;

  constructor(private http: HttpClient) { }

  public getUserConfigs() {
    return this.http.get<ApiResponse<UserConfigDetailDto<string>[]>>(`${this.url}`);
  }

  public updateUserConfigs(userConfigs: UserConfigUpdateDto<string>[]) {
    return this.http.post<ApiResponse<UserConfigDetailDto<string>[]>>(`${this.url}`, userConfigs);
  }
}
