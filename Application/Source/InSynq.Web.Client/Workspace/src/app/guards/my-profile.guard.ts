import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const myProfileGuard: CanActivateFn = (route, state) => {
  const currentUser = inject(AuthService).getCurrentUser();
  const userId = +(route.paramMap.get('id'))!;

  const isMyProfile = !!currentUser && userId === currentUser.id;

  if (isMyProfile)
    return true;
  else {
    inject(Router).navigateByUrl('/unauthorized');
    return false;
  }

};
