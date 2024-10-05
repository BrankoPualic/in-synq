import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';

@Component({
  selector: 'app-profile-terms-of-use',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-terms-of-use.component.html',
  styleUrl: './profile-terms-of-use.component.scss'
})
export class ProfileTermsOfUseComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
