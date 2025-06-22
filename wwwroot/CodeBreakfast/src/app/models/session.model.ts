import {AppRole} from './user.model';

export interface SessionModel{
  username: string;
  accessToken: string;
  roles: AppRole[];
}
