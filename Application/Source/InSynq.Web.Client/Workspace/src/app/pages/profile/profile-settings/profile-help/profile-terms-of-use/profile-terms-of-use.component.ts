import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { AuthService } from '../../../../../services/auth.service';
import { ToastService } from '../../../../../services/toast.service';
import { DocumentController } from '../../../../../_generated/services';
import { eLegalDocumentType } from '../../../../../_generated/enums';
import { IDocumentDto } from '../../../../../_generated/interfaces';

@Component({
  selector: 'app-profile-terms-of-use',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-terms-of-use.component.html',
  styleUrl: './profile-terms-of-use.component.scss'
})
export class ProfileTermsOfUseComponent extends BaseProfileSettingsComponent {
  document?: IDocumentDto;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private documentController: DocumentController
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)

    this.loading = true;
    this.documentController.GetByType(eLegalDocumentType.TermsOfUse).toPromise()
      .then(_ => this.document = _!)
      .catch(_ => this.error(_.erorr.errors))
      .finally(() => this.loading = false)
  }
}
