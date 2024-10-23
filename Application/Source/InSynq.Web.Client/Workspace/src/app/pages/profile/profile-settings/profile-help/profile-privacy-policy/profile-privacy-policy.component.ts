import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { AuthService } from '../../../../../services/auth.service';
import { ToastService } from '../../../../../services/toast.service';
import { eLegalDocumentType } from '../../../../../_generated/enums';
import * as api from '../../../../../api';

@Component({
  selector: 'app-profile-privacy-policy',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-privacy-policy.component.html',
  styleUrl: './profile-privacy-policy.component.scss'
})
export class ProfilePrivacyPolicyComponent extends BaseProfileSettingsComponent {
  document?: api.DocumentDto;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private api_DocumentController: api.DocumentController
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)

    this.loading = true;
    this.api_DocumentController.GetByType(eLegalDocumentType.PrivacyPolicy).toPromise()
      .then(_ => this.document = _)
      .catch(_ => this.error(_.erorr.errors))
      .finally(() => this.loading = false)
  }

}
