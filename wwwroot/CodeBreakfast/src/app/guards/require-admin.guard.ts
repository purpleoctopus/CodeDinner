import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {map, take} from 'rxjs';
import {AppRole} from '../models/user.model';

export const requireAdminGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  return auth.sessionData.pipe(
    take(1),
    map(session => {
      const isAdmin = session?.roles.includes(AppRole.Admin);
      return isAdmin ? true : router.parseUrl('/no-access');
    })
  );
};
