import { Component } from '@angular/core';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseComponent } from '../../base/base.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [GLOBAL_MODULES],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent extends BaseComponent {

}
