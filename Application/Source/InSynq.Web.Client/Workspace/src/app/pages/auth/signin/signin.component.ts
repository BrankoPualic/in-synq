import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../base/base-form.component';
import { ISigninDto } from '../../../_generated/interfaces';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { AuthService } from '../../../services/auth.service';
import { ToastService } from '../../../services/toast.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AuthController } from '../../../_generated/services';
import { faEye, faEyeSlash } from '@fortawesome/free-solid-svg-icons';
import { HttpErrorResponse } from '@angular/common/http';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { ValidationDirective } from '../../../directives/validation.directive';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [GLOBAL_MODULES, ReactiveFormsModule, ValidationDirective],
  templateUrl: './signin.component.html',
  styleUrl: './signin.component.scss'
})
export class SigninComponent extends BaseFormComponent<ISigninDto> implements OnInit {
  isPasswordVisible = false;
  currentIcon = this.Icons.EYE_SLASH;

  constructor
    (
      errorService: ErrorService,
      loaderService: PageLoaderService,
      authService: AuthService,
      toastService: ToastService,
      fb: FormBuilder,
      private authController: AuthController
    ) {
    super(errorService, loaderService, authService, toastService, fb);
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  // Base component methods

  protected override initializeForm(): void {
    this.form = this.fb.group({
      [this.nameof(_ => _.email)]: [''],
      [this.nameof(_ => _.password)]: ['']
    })
  }
  protected override submit(): void {
    this.loading = true;
    this.cleanErrors();

    this.authController.Signin(this.form.value).toPromise()
      .then(_ => { if (_) this.authService.setUser(_) })
      .catch((_: HttpErrorResponse) => this.errorService.add(_.error.errors))
      .finally(() => this.loading = false);
  }

  // methods

  togglePassword(password: HTMLInputElement): void {
    this.isPasswordVisible = !this.isPasswordVisible;
    password.type = this.isPasswordVisible ? 'text' : 'password';
    this.currentIcon = this.isPasswordVisible ? this.Icons.EYE : this.Icons.EYE_SLASH;
  }

}
