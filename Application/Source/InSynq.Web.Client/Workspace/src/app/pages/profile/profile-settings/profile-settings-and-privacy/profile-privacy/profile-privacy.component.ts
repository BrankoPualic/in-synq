import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile-privacy',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-privacy.component.html',
  styleUrl: './profile-privacy.component.scss'
})
export class ProfilePrivacyComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
