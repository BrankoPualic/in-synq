import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';

@Component({
  selector: 'app-profile-help-desk',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-help-desk.component.html',
  styleUrl: './profile-help-desk.component.scss'
})
export class ProfileHelpDeskComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
