import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { GLOBAL_MODULES } from '../../../../../_global.modules';

@Component({
  selector: 'app-profile-saved',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-saved.component.html',
  styleUrl: './profile-saved.component.scss'
})
export class ProfileSavedComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
