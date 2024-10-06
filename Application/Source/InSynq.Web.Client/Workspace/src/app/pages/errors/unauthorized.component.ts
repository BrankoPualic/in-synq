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
    <div class="d-column align-items-center pt-3">
      <h2>Unauthorized!</h2>
      <h3>401</h3>
      <button class="btn btn-primary d-row align-items-center" (click)="goBack()">
      <span [class]="Icons.NG_ANGLE_LEFT" class="me-2" style="font-size: 1.1rem;"></span> Go Back
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
