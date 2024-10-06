import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { AuthService } from '../../../../../services/auth.service';
import { ToastService } from '../../../../../services/toast.service';
import { IUserDto, IUserLogDto } from '../../../../../_generated/interfaces';
import { ProfileService } from '../../../../../services/profile.service';
import { take } from 'rxjs';
import { UserController } from '../../../../../_generated/services';
import { HttpErrorResponse } from '@angular/common/http';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-profile-about',
  standalone: true,
  imports: [GLOBAL_MODULES, DialogModule],
  templateUrl: './profile-about.component.html',
  styleUrl: './profile-about.component.scss'
})
export class ProfileAboutComponent extends BaseProfileSettingsComponent implements OnInit {
  userLog: IUserLogDto | null = null;
  usernamesVisible = false;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private profileService: ProfileService,
    private userController: UserController,
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  ngOnInit(): void {
    this.profileService.$profile.pipe(take(1)).subscribe(_ => this.profile = _);

    this.loading = true;
    this.userController.GetUserLog(this.userId).toPromise()
      .then(_ => this.userLog = _)
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }

  showUsernames = (): void => { this.usernamesVisible = true; }
}
