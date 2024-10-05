import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';

@Component({
  selector: 'app-profile-privacy-policy',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-privacy-policy.component.html',
  styleUrl: './profile-privacy-policy.component.scss'
})
export class ProfilePrivacyPolicyComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
