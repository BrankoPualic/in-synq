import { Injectable } from '@angular/core';
import { ICurrentUser } from '../models/current-user.model';
import { Router } from '@angular/router';
import { ITokenDto } from '../_generated/interfaces';
import { DateTime } from 'luxon';
import { eSystemRole } from '../_generated/enums';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private router: Router
  ) { }

  signout() {
    localStorage.removeItem('token');
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
      tokenExpirationDate: DateTime.fromMillis(tokenInfo.exp * 1000).toJSDate()
    };

    if (Array.isArray(tokenInfo.ROLES))
      tokenInfo.ROLES.forEach((_: string) => userSource.roles?.push(Number(_)));
    else
      userSource.roles?.push(tokenInfo.ROLES);

    localStorage.setItem('token', data.token);
    this.router.navigateByUrl('/');
  }

  getCurrentUser(): ICurrentUser | null {
    const token = this.getToken();
    if (!token) {
      this.signout();
      return null;
    }

    const tokenInfo = this.getDecodedToken(token);

    return {
      id: tokenInfo.ID,
      roles: [],
      email: tokenInfo.EMAIL,
      username: tokenInfo.USERNAME,
      token: token,
      tokenExpirationDate: DateTime.fromMillis(tokenInfo.exp * 1000).toJSDate()
    }
  }

  getToken(): string | null {
    const token = localStorage.getItem('token');
    if (!token)
      return null;

    const decodedToken = this.getDecodedToken(token);
    if (!decodedToken.exp)
      return null;

    const currentTimestamp = DateTime.now().toSeconds();
    if (currentTimestamp >= decodedToken.exp)
      return null;

    return token;
  }

  hasAccess(...roles: eSystemRole[]): boolean {
    const token = this.getToken();
    if (!token)
      return false;

    const userRoles: number[] = [];
    var decodedToken = this.getDecodedToken(token);
    if (Array.isArray(decodedToken.ROLES))
      decodedToken.ROLES.forEach((_: string) => userRoles.push(Number(_)));
    else
      userRoles.push(decodedToken.ROLES);

    if (userRoles.length < 1)
      return false;

    return roles.some(_ => userRoles.includes(_));
  }

  // private

  private getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
