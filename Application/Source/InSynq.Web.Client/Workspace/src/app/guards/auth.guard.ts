import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constants } from '../constants/constants';
import { StorageService } from '../services/storage.service';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const storageService = inject(StorageService);

  const token = storageService.get('token');
  if (!token) {
    router.navigateByUrl(Constants.ROUTE_AUTH_SIGNIN);
    return false;
  }

  return true;
};
