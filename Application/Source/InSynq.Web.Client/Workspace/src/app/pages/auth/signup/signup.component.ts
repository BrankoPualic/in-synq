import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { ICountryDto, IEnumProvider, ISignupDto } from '../../../_generated/interfaces';
import { Providers } from '../../../_generated/providers';
import { AuthController, ProviderController } from '../../../_generated/services';
import { BaseFormComponent } from '../../../base/base-form.component';
import { RequiredFieldMarkComponent } from "../../../components/required-field-mark.component";
import { ValidationDirective } from '../../../directives/validation.directive';
import { IBasicObject } from '../../../models/models';
import { AuthService } from '../../../services/auth.service';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { ToastService } from '../../../services/toast.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [GLOBAL_MODULES, ReactiveFormsModule, RequiredFieldMarkComponent, CalendarModule, DropdownModule, ValidationDirective, FileUploadModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss'
})
export class SignupComponent extends BaseFormComponent<ISignupDto> implements OnInit {
  image?: File;
  countries: ICountryDto[] = [];
  countryLoader = false;
  genders: IEnumProvider[] = [];
  isPasswordVisible = false;
  currentIcon = this.Icons.NG_EYE_SLASH;
  currentCountry?: ICountryDto;
  currentGender?: IBasicObject;

  constructor
    (
      errorService: ErrorService,
      loaderService: PageLoaderService,
      authService: AuthService,
      toastService: ToastService,
      router: Router,
      fb: FormBuilder,
      private providerController: ProviderController,
      private authController: AuthController,
      private providers: Providers
    ) {
    super(errorService, loaderService, authService, toastService, router, fb);
  }

  ngOnInit(): void {
    this.initializeForm();

    this.genders = this.providers.getGenders();

    this.countryLoader = true;
    this.providerController.GetCountries().toPromise()
      .then(_ => this.countries = _ ?? [])
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.countryLoader = false);
  }
  // Base component methods

  protected override initializeForm(): void {
    this.form = this.fb.group({
      [this.nameof(_ => _.firstName)]: [''],
      [this.nameof(_ => _.middleName)]: [''],
      [this.nameof(_ => _.lastName)]: [''],
      [this.nameof(_ => _.username)]: [''],
      [this.nameof(_ => _.email)]: [''],
      [this.nameof(_ => _.password)]: [''],
      [this.nameof(_ => _.confirmPassword)]: [''],
      [this.nameof(_ => _.dateOfBirth)]: [''],
      [this.nameof(_ => _.biography)]: [''],
      [this.nameof(_ => _.genderId)]: [0],
      [this.nameof(_ => _.phone)]: [''],
      [this.nameof(_ => _.countryId)]: [0],
      [this.nameof(_ => _.privacy)]: [false],
    });
  }

  protected override submit(): void {
    this.toFormData(this.form.value);
    this.loading = true;
    this.cleanErrors();

    this.authController.Signup(this.formData).toPromise()
      .then(_ => { if (_) this.authService.setUser(_) })
      .catch((_: HttpErrorResponse) => { this.formData = new FormData(); this.errorService.add(_.error.errors) })
      .finally(() => this.loading = false);
  }

  protected override toFormData(formValueObj: ISignupDto): void {
    if (formValueObj.dateOfBirth.toString() === '')
      formValueObj.dateOfBirth = new Date();

    this.formData.append(this.nameof(_ => _.firstName), formValueObj.firstName);
    this.formData.append(this.nameof(_ => _.middleName), formValueObj.middleName);
    this.formData.append(this.nameof(_ => _.lastName), formValueObj.lastName);
    this.formData.append(this.nameof(_ => _.username), formValueObj.username);
    this.formData.append(this.nameof(_ => _.email), formValueObj.email);
    this.formData.append(this.nameof(_ => _.password), formValueObj.password);
    this.formData.append(this.nameof(_ => _.confirmPassword), formValueObj.confirmPassword);
    this.formData.append(this.nameof(_ => _.dateOfBirth), formValueObj.dateOfBirth.toDateString());
    this.formData.append(this.nameof(_ => _.biography), formValueObj.biography);
    this.formData.append(this.nameof(_ => _.countryId), formValueObj.countryId.toString());
    this.formData.append(this.nameof(_ => _.genderId), formValueObj.genderId.toString());
    this.formData.append(this.nameof(_ => _.phone), formValueObj.phone);
    this.formData.append(this.nameof(_ => _.privacy), formValueObj.privacy.toString());

    if (this.image) {
      this.formData.append(
        this.nameof(_ => _.photo),
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
    this.form.get(this.nameof(_ => _.genderId))?.setValue(this.currentGender?.id);
  }

  getCountry(id: number): ICountryDto {
    return this.countries.find(_ => _.id === id) || {} as ICountryDto;
  }

  onCountryChange(event: DropdownChangeEvent): void {
    const country = this.countries.find(_ => _.id === event.value.id) || {} as ICountryDto;
    this.currentCountry = event.value;
    this.form.get(this.nameof(_ => _.countryId))?.setValue(country.id);
    this.form.get(this.nameof(_ => _.phone))?.setValue(country.dialCode + ' ');
  }

  onFileSelect(input: FileSelectEvent): void {
    if (input.currentFiles && input.currentFiles[0])
      this.image = input.currentFiles[0];
  }
}
