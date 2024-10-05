import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';

@Component({
  selector: 'app-add-profile',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './add-profile.component.html',
  styleUrl: './add-profile.component.scss'
})
export class AddProfileComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
