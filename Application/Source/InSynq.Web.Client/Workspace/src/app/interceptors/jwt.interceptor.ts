import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const service = inject(AuthService);

  const token = service.getToken();

  if (!token)
    service.signout();
  else
    req = req.clone({
      headers: req.headers.set('Auhtorization', `Bearer ${token}`)
    })

  return next(req);
};
