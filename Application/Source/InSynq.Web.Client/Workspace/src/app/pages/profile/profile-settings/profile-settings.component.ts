import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../base/base-profile-settings.component';

@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-settings.component.html',
  styleUrl: './profile-settings.component.scss'
})
export class ProfileSettingsComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}