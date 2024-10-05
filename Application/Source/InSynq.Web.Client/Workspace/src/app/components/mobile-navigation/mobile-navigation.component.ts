import { Component } from '@angular/core';
import { IconConstants } from '../../constants/icon-constants';
import { Constants } from '../../constants/constants';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { ICurrentUser } from '../../models/current-user.model';
import { AuthService } from '../../services/auth.service';

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
  currentUser: ICurrentUser | null = null;

  constructor(private authService: AuthService) {
    this.currentUser = authService.getCurrentUser();
  }
}
