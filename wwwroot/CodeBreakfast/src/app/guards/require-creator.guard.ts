import {CanActivateFn, Router} from '@angular/router';
import {inject} from '@angular/core';
import {AuthService} from '../services/auth.service';
import {map, take} from 'rxjs';
import {AppRole} from '../models/user.model';

export const requireCreatorGuard: CanActivateFn = (route, state) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  return auth.sessionData.pipe(
    take(1),
    map(session => {
      const isCreator = session?.roles.includes(AppRole.Creator) || session?.roles.includes(AppRole.Admin);
      return isCreator ? true : router.parseUrl('/no-access');
    })
  );
};
