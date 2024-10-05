import { Component } from '@angular/core';
import { BaseProfileSettingsComponent } from '../../../../base/base-profile-settings.component';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { GLOBAL_MODULES } from '../../../../../_global.modules';

@Component({
  selector: 'app-profile-report',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-report.component.html',
  styleUrl: './profile-report.component.scss'
})
export class ProfileReportComponent extends BaseProfileSettingsComponent {
  constructor(location: Location, route: ActivatedRoute) {
    super(location, route)
  }
}
