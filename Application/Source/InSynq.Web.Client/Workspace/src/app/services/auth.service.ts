import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { DateTime } from 'luxon';
import { eSystemRole } from '../_generated/enums';
import { ITokenDto, IUserDto } from '../_generated/interfaces';
import { UserController } from '../_generated/services';
import { ICurrentUser } from '../models/current-user.model';
import { PageLoaderService } from './page-loader.service';
import { ProfileService } from './profile.service';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(
    private router: Router,
    private storageService: StorageService,
    private loaderService: PageLoaderService,
    private profileService: ProfileService,
    private userController: UserController
  ) { }

  loadCurrentUser(): void {
    this.loaderService.show();
    this.userController.GetCurrentUser().toPromise()
      .then(_ => this.profileService.setProfile(_!))
      .finally(() => this.loaderService.hide());
  }

  loadCurrentUserAsPrmoise = (): Promise<IUserDto | null> => this.userController.GetCurrentUser().toPromise();

  signout() {
    this.storageService.remove('token');
    this.router.navigateByUrl('/');
  }

  setUser(data: ITokenDto) {
    this.storageService.set('token', data.token);
    this.router.navigateByUrl('/');
  }

  getCurrentUser(): ICurrentUser | null {
    const token = this.getToken();
    if (!token)
      return null;

    const tokenInfo = this.getDecodedToken(token);

    return {
      id: Number(tokenInfo.ID),
      roles: [],
      email: tokenInfo.EMAIL,
      username: tokenInfo.USERNAME,
      token: token,
      tokenExpirationDate: DateTime.fromMillis(tokenInfo.exp * 1000).toJSDate()
    }
  }

  getToken(): string | null {
    const token = this.storageService.get('token');
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
