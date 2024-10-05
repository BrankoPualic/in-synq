import { Component } from '@angular/core';
import { GLOBAL_MODULES } from '../../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile-archive',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-archive.component.html',
  styleUrl: './profile-archive.component.scss'
})
export class ProfileArchiveComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
