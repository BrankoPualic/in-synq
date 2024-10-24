import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { BaseFormComponent } from '../../../base/base-form.component';
import { ValidationDirective } from '../../../directives/validation.directive';
import { AuthService } from '../../../services/auth.service';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { ToastService } from '../../../services/toast.service';
import { Router } from '@angular/router';
import * as api from '../../../api';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [GLOBAL_MODULES, ReactiveFormsModule, ValidationDirective],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss'
})
export class SigninComponent extends BaseFormComponent<api.SigninDto> implements OnInit {
  isPasswordVisible = false;
  currentIcon = this.Icons.NG_EYE_SLASH;

  constructor
    (
      errorService: ErrorService,
      loaderService: PageLoaderService,
      authService: AuthService,
      toastService: ToastService,
      router: Router,
      fb: FormBuilder,
      private api_AuthController: api.AuthController
    ) {
    super(errorService, loaderService, authService, toastService, router, fb);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  // Base component methods

  protected override initializeForm(): void {
    this.form = this.fb.group({
      [this.nameof(_ => _.Email)]: [''],
      [this.nameof(_ => _.Password)]: ['']
    })
  }
  protected override submit(): void {
    this.loading = true;
    this.cleanErrors();

    this.api_AuthController.Signin(this.form.value).toPromise()
      .then(_ => { if (_) this.authService.setUser(_) })
      .catch((_: HttpErrorResponse) => this.errorService.add(_.error.errors))
      .finally(() => this.loading = false);
  }

  // methods

  togglePassword(password: HTMLInputElement): void {
    this.isPasswordVisible = !this.isPasswordVisible;
    password.type = this.isPasswordVisible ? 'text' : 'password';
    this.currentIcon = this.isPasswordVisible ? this.Icons.NG_EYE : this.Icons.NG_EYE_SLASH;
  }

}
