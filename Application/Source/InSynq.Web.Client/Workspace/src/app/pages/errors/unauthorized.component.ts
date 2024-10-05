import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseConstants } from '../../models/base-component.model';
import { Location } from '@angular/common';

@Component({
  selector: 'app-unauthorized',
  standalone: true,
  imports: [GLOBAL_MODULES, RouterLink],
  template: `
    <div class="text-center">
      <h2>Unauthorized!</h2>
      <h3>401</h3>
      <button class="btn btn-primary" (click)="goBack()">
        <fa-icon [icon]="Icons.ANGLE_LEFT"></fa-icon> Go Back
      </button>
    </div>
  `,
  styles: ``,
})
export class UnauthorizedComponent extends BaseConstants {
  constructor(private location: Location) {
    super()
  }

  goBack = (): void => this.location.back();
}
