import { Component } from '@angular/core';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { BaseConstants } from '../../../models/base-component.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-profile-settings',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './profile-settings.component.html',
  styleUrl: './profile-settings.component.scss'
})
export class ProfileSettingsComponent extends BaseConstants {
  constructor(private location: Location) {
    super()
  }

  goBack = () => this.location.back();
}