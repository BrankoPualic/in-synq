import { Component, OnInit } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../../../../../services/profile.service';
import { Subject, take, takeUntil } from 'rxjs';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { AuthService } from '../../../../../services/auth.service';
import { ToastService } from '../../../../../services/toast.service';
import { HttpErrorResponse } from '@angular/common/http';
import { QueueService } from '../../../../../services/queue.service';
import * as api from '../../../../../api';

@Component({
  selector: 'app-profile-privacy',
  standalone: true,
  imports: [GLOBAL_MODULES, InputSwitchModule],
  templateUrl: './profile-privacy.component.html',
  styleUrl: './profile-privacy.component.scss'
})
export class ProfilePrivacyComponent extends BaseProfileSettingsComponent implements OnInit {
  private _cpDestroy$ = new Subject<void>();
  privacy = false;
  isChanged = false;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private $q: QueueService,
    private profileService: ProfileService,
    private api_UserController: api.UserController
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  ngOnInit(): void {
    this.profileService.profile$.pipe(takeUntil(this._cpDestroy$)).subscribe(_ => {
      this.profile = _;
      this.privacy = _!.Privacy;
    });
  }

  override ngOnDestroy(): void {
    this._cpDestroy$.next();
    this._cpDestroy$.complete();
  }

  override goBack(): void {
    if (!!this.profile)
      this.profile.Privacy = this.privacy;

    if (!this.isChanged) {
      this.location.back();
      return;
    }

    this.loading = true;
    this.$q.sequential([
      () => this.api_UserController.Update(this.profile ?? new api.UserDto()).toPromise(),
      () => this.authService.loadCurrentUserAsPrmoise()
    ])
      .then((result) => {
        this.profileService.setProfile(result[1]);
        this.location.back();
      })
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }

  onPrivacyChanged = (): void => { this.isChanged = !this.isChanged };
}
