import { CanActivateFn, Router } from '@angular/router';
import {inject} from '@angular/core';
import { AuthService } from '../services/auth.service';
import {firstValueFrom, take} from 'rxjs';

export const requireAccessTokenGuard: CanActivateFn = async (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const sessionData = await firstValueFrom(authService.sessionData.pipe(take(1)));

  if(!sessionData?.accessToken) {
    await router.navigate(['/no-access']);
    return false;
  }

  return true;
};
