import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { inject } from '@angular/core';
import { IUser } from '../types/user';
import { map } from 'rxjs';

export const AnonymousGuard: CanActivateFn = (route, state) => {

  const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  return authService.currentUser$.pipe(
    map((user: IUser | null) => {
      if(user && localStorage.getItem('token')) {
        router.navigateByUrl('/');
        return false;
      }
      return true;
    })
  )
};
