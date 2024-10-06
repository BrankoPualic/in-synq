import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { CustomConfirmationService } from '../../../../services/custom-confirmation.service';
import { AuthService } from '../../../../services/auth.service';

@Component({
  selector: 'app-profile-settings-and-privacy',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-settings-and-privacy.component.html',
  styleUrl: './profile-settings-and-privacy.component.scss'
})
export class ProfileSettingsAndPrivacyComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute, private confirmationService: CustomConfirmationService, private authService: AuthService) {
    super(location, route)
  }

  signout(): void {
    this.confirmationService.confirm('Are you sure you want to sign out?').result
      .then(() => this.authService.signout());
  }
}
