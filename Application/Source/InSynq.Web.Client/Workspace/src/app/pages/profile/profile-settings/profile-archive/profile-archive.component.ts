import { Component } from '@angular/core';
import { GLOBAL_MODULES } from '../../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '../../../../services/error.service';
import { PageLoaderService } from '../../../../services/page-loader.service';
import { AuthService } from '../../../../services/auth.service';
import { ToastService } from '../../../../services/toast.service';

@Component({
  selector: 'app-profile-archive',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-archive.component.html',
  styleUrl: './profile-archive.component.scss'
})
export class ProfileArchiveComponent extends BaseProfileSettingsComponent {
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