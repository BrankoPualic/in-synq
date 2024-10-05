import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseConstants } from '../../models/base-component.model';
import { Location } from '@angular/common';
@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [GLOBAL_MODULES, RouterLink],
  template: `
    <div class="text-center">
      <h2>Page Not Found!</h2>
      <h3>404</h3>
      <button class="btn btn-primary" (click)="goBack()">
        <span [class]="Icons.NG_ANGLE_LEFT" style="font-size: 1.1rem;"></span> Go Back
      </button>
    </div>
  `,
  styles: ``,
})
export class NotFoundComponent extends BaseConstants {
  constructor(private location: Location) {
    super()
  }

  goBack = (): void => this.location.back();
}