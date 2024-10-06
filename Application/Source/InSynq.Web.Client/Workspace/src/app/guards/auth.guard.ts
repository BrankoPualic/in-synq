import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constants } from '../constants/constants';
import { StorageService } from '../services/storage.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const storageService = inject(StorageService);

  const url = state.url;
  const token = storageService.get('token');

  if (typeof window !== 'undefined') {
    if (!token && !url.includes('auth')) {
      router.navigateByUrl(Constants.ROUTE_PATH_AUTH);
      return false;
    }
    else if (token && url.includes('auth')) {
      router.navigateByUrl(Constants.ROUTE_PATH_HOME)
      return false;
    }

    return true;
  }

  return false;
};
