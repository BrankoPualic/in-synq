import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ICurrentUser } from '../models/current-user.model';
import { Router } from '@angular/router';
import { ITokenDto } from '../_generated/interfaces';
import { DateTime } from 'luxon';
import { eSystemRole } from '../_generated/enums';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _currentUserSource = new BehaviorSubject<ICurrentUser | null>(null);
  currentUser$ = this._currentUserSource.asObservable();

  constructor(
    private router: Router
  ) { }

  signout() {
    localStorage.removeItem('token');
    this._currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  setUser(data: ITokenDto) {
    const tokenInfo = this.getDecodedToken(data.token);

    const userSource: ICurrentUser = {
      id: tokenInfo.ID,
      roles: [],
      email: tokenInfo.EMAIL,
      username: tokenInfo.USERNAME,
      token: data.token,
      tokenExpiryDate: DateTime.fromMillis(tokenInfo.exp * 1000).toJSDate()
    };

    if (Array.isArray(tokenInfo.ROLES))
      tokenInfo.ROLES.forEach((_: string) => userSource.roles?.push(Number(_)));
    else
      userSource.roles?.push(tokenInfo.ROLES);

    localStorage.setItem('token', data.token);
    this._currentUserSource.next(userSource);
    this.router.navigateByUrl('/');
  }

  hasAccess(...roles: eSystemRole[]): boolean {
    const user = this._currentUserSource.getValue();
    if (!user?.roles)
      return false;

    return roles.some(_ => user.roles?.includes(_));
  }

  // private

  private getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
