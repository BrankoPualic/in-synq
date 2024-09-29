import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { take } from 'rxjs';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  inject(AuthService)
    .currentUser$.pipe(take(1))
    .subscribe(_ => {
      if (_)
        req = req.clone({
          headers: req.headers.set('Auhtorization', `Bearer ${_.token}`)
        })
    });

  return next(req);
};
