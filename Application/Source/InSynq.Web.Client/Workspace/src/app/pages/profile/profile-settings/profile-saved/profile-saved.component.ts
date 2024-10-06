import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { GLOBAL_MODULES } from '../../../../../_global.modules';
import { ErrorService } from '../../../../services/error.service';
import { PageLoaderService } from '../../../../services/page-loader.service';
import { AuthService } from '../../../../services/auth.service';
import { ToastService } from '../../../../services/toast.service';

@Component({
  selector: 'app-profile-saved',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-saved.component.html',
  styleUrl: './profile-saved.component.scss'
})
export class ProfileSavedComponent extends BaseProfileSettingsComponent {
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
}
