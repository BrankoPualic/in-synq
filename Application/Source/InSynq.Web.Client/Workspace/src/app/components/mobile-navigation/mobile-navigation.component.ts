import { Component } from '@angular/core';
import { IconConstants } from '../../constants/icon-constants';
import { Constants } from '../../constants/constants';
import { GLOBAL_MODULES } from '../../../_global.modules';

@Component({
  selector: 'app-mobile-navigation',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './mobile-navigation.component.html',
  styleUrl: './mobile-navigation.component.scss'
})
export class MobileNavigationComponent {
  Icons = IconConstants;
  Constants = Constants;
}
