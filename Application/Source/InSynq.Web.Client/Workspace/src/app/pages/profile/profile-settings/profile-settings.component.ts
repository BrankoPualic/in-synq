import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../base/base-profile-settings.component';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { AuthService } from '../../../services/auth.service';
import { ToastService } from '../../../services/toast.service';

@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-settings.component.html',
  styleUrl: './profile-settings.component.scss'
})
export class ProfileSettingsComponent extends BaseProfileSettingsComponent {
  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  isMyProfile = !!this.currentUser && this.userId === this.currentUser.id;
}