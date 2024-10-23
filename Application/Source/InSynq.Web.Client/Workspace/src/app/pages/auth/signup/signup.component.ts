import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CalendarModule } from 'primeng/calendar';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { BaseFormComponent } from '../../../base/base-form.component';
import { CountryDropdownComponent } from "../../../components/dropdown/country-dropdown/country-dropdown.component";
import { DropdownComponent } from '../../../components/dropdown/dropdown.component';
import { LookupDropdownComponent } from "../../../components/dropdown/lookup-dropdown/lookup-dropdown.component";
import { LoaderComponent } from '../../../components/loader.component';
import { RequiredFieldMarkComponent } from "../../../components/required-field-mark.component";
import { ValidationDirective } from '../../../directives/validation.directive';
import { AuthService } from '../../../services/auth.service';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { ToastService } from '../../../services/toast.service';
import * as api from '../../../api';
@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [GLOBAL_MODULES, ReactiveFormsModule, RequiredFieldMarkComponent, CalendarModule, DropdownModule, ValidationDirective, FileUploadModule, LoaderComponent, CountryDropdownComponent, DropdownComponent, LookupDropdownComponent],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent extends BaseFormComponent<api.SignupDto> implements OnInit {
  image?: File;
  isPasswordVisible = false;
  currentIcon = this.Icons.NG_EYE_SLASH;
  currentCountry?: api.CountryDto;
  currentGender?: api.EnumProvider;

  constructor
    (
      errorService: ErrorService,
      loaderService: PageLoaderService,
      authService: AuthService,
      toastService: ToastService,
      router: Router,
      fb: FormBuilder,
      private api_AuthController: api.AuthController,
    ) {
    super(errorService, loaderService, authService, toastService, router, fb);
  }

  ngOnInit(): void {
    this.initializeForm();
  }
  // Base component methods

  protected override initializeForm(): void {
    this.form = this.fb.group({
      [this.nameof(_ => _.FirstName)]: [''],
      [this.nameof(_ => _.MiddleName)]: [''],
      [this.nameof(_ => _.LastName)]: [''],
      [this.nameof(_ => _.Username)]: [''],
      [this.nameof(_ => _.Email)]: [''],
      [this.nameof(_ => _.Password)]: [''],
      [this.nameof(_ => _.ConfirmPassword)]: [''],
      [this.nameof(_ => _.DateOfBirth)]: [''],
      [this.nameof(_ => _.Biography)]: [''],
      [this.nameof(_ => _.GenderId)]: [0],
      [this.nameof(_ => _.Phone)]: [''],
      [this.nameof(_ => _.CountryId)]: [0],
      [this.nameof(_ => _.Privacy)]: [false],
    });
  }

  protected override submit(): void {
    this.toFormData(this.form.value);
    this.loading = true;
    this.cleanErrors();

    this.api_AuthController.Signup(this.formData).toPromise()
      .then(_ => { if (_) this.authService.setUser(_) })
      .catch((_: HttpErrorResponse) => { this.formData = new FormData(); this.errorService.add(_.error.errors) })
      .finally(() => this.loading = false);
  }

  protected override toFormData(formValueObj: api.SignupDto): void {
    if (formValueObj.DateOfBirth.toString() === '')
      formValueObj.DateOfBirth = new Date();

    this.formData.append(this.nameof(_ => _.FirstName), formValueObj.FirstName);
    this.formData.append(this.nameof(_ => _.MiddleName), formValueObj.MiddleName);
    this.formData.append(this.nameof(_ => _.LastName), formValueObj.LastName);
    this.formData.append(this.nameof(_ => _.Username), formValueObj.Username);
    this.formData.append(this.nameof(_ => _.Email), formValueObj.Email);
    this.formData.append(this.nameof(_ => _.Password), formValueObj.Password);
    this.formData.append(this.nameof(_ => _.ConfirmPassword), formValueObj.ConfirmPassword);
    this.formData.append(this.nameof(_ => _.DateOfBirth), formValueObj.DateOfBirth.toDateString());
    this.formData.append(this.nameof(_ => _.Biography), formValueObj.Biography);
    this.formData.append(this.nameof(_ => _.CountryId), formValueObj.CountryId.toString());
    this.formData.append(this.nameof(_ => _.GenderId), formValueObj.GenderId.toString());
    this.formData.append(this.nameof(_ => _.Phone), formValueObj.Phone);
    this.formData.append(this.nameof(_ => _.Privacy), formValueObj.Privacy.toString());

    if (this.image) {
      this.formData.append(
        this.nameof(_ => _.Photo),
        this.image,
        this.image?.name,
      );
    }
  }

  // methods

  togglePassword(password: HTMLInputElement, confirmPassword: HTMLInputElement): void {
    this.isPasswordVisible = !this.isPasswordVisible;
    password.type = this.isPasswordVisible ? 'text' : 'password';
    confirmPassword.type = this.isPasswordVisible ? 'text' : 'password';
    this.currentIcon = this.isPasswordVisible ? this.Icons.NG_EYE : this.Icons.NG_EYE_SLASH;
  }

  onGenderChange(): void {
    this.form.get(this.nameof(_ => _.GenderId))?.setValue(this.currentGender?.Id);
  }

  onCountryChange(event: DropdownChangeEvent): void {
    this.currentCountry = event.value;
    this.form.get(this.nameof(_ => _.CountryId))?.setValue(this.currentCountry?.Id);
    this.form.get(this.nameof(_ => _.Phone))?.setValue(this.currentCountry?.DialCode + ' ');
  }

  onFileSelect(input: FileSelectEvent): void {
    if (input.currentFiles && input.currentFiles[0])
      this.image = input.currentFiles[0];
  }
}
