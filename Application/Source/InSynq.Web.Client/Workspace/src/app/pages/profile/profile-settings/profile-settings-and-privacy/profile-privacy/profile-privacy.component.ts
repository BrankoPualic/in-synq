import { Component, OnInit } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../../../../../services/profile.service';
import { take } from 'rxjs';
import { IUserDto } from '../../../../../_generated/interfaces';
import { InputSwitchModule } from 'primeng/inputswitch';
import { UserController } from '../../../../../_generated/services';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { AuthService } from '../../../../../services/auth.service';
import { ToastService } from '../../../../../services/toast.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-profile-privacy',
  standalone: true,
  imports: [GLOBAL_MODULES, InputSwitchModule],
  templateUrl: './profile-privacy.component.html',
  styleUrl: './profile-privacy.component.scss'
})
export class ProfilePrivacyComponent extends BaseProfileSettingsComponent implements OnInit {
  profile: IUserDto | null = null;
  privacy = false;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private profileService: ProfileService,
    private userController: UserController
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  ngOnInit(): void {
    this.profileService.$profile.pipe(take(1)).subscribe(_ => {
      this.profile = _;
      this.privacy = _?.privacy!;
    });
  }

  override goBack(): void {
    if (!!this.profile)
      this.profile.privacy = this.privacy;

    this.loading = true;
    this.userController.Update(this.profile ?? {} as IUserDto).toPromise()
      .then(() => {
        this.userController.GetSingle(this.profile!.id).toPromise()
          .then(_ => {
            this.profileService.setProfile(_!);
            this.location.back();
          })
          .catch((_: HttpErrorResponse) => this.error(_.error.errors));
      })
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }
}