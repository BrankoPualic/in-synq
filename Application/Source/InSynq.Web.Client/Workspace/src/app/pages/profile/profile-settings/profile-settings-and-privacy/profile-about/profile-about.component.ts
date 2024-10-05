import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';

@Component({
  selector: 'app-profile-about',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-about.component.html',
  styleUrl: './profile-about.component.scss'
})
export class ProfileAboutComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
