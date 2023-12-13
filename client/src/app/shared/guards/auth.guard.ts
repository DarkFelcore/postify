import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';
import { IUser } from '../types/user';

export const AuthGuard: CanActivateFn = (route, state) => {
  const authService: AuthService = inject(AuthService);
  const router: Router = inject(Router);

  return authService.currentUser$.pipe(
    map((user: IUser | null) => {
      if(user && localStorage.getItem('token')) return true;
      router.navigateByUrl('/auth')
      return false;
    })
  )
};
