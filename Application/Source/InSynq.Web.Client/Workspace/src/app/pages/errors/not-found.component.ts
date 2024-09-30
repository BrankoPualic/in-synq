import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseConstants } from '../../models/base-component.model';
@Component({
    selector: 'app-not-found',
    standalone: true,
    imports: [GLOBAL_MODULES, RouterLink],
    template: `
    <div class="text-center">
      <h2>Page Not Found!</h2>
      <h3>404</h3>
      <button class="btn btn-primary" [routerLink]="[Constants.ROUTE_HOME]">
        <fa-icon [icon]="Icons.ARROW_LEFT_LONG"></fa-icon> Go Back
      </button>
    </div>
  `,
    styles: ``,
})
export class NotFoundComponent extends BaseConstants {

}