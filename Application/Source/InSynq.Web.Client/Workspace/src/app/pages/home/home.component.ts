import { Component } from '@angular/core';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseComponent } from '../../base/base.component';
import { MobileNavigationComponent } from "../../components/mobile-navigation/mobile-navigation.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GLOBAL_MODULES, MobileNavigationComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent extends BaseComponent {

}
