import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../_global.modules';

@Component({
  selector: 'app-profile-help',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-help.component.html',
  styleUrl: './profile-help.component.scss'
})
export class ProfileHelpComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
