import { Component, OnInit } from '@angular/core';
import { BaseFormComponent } from '../../../base/base-form.component';
import { ICountryDto, IEnumProvider, ISignupDto } from '../../../_generated/interfaces';
import { ErrorService } from '../../../services/error.service';
import { PageLoaderService } from '../../../services/page-loader.service';
import { AuthService } from '../../../services/auth.service';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AuthController, ProviderController } from '../../../_generated/services';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastService } from '../../../services/toast.service';
import { GLOBAL_MODULES } from '../../../../_global.modules';
import { RequiredFieldMarkComponent } from "../../../components/required-field-mark.component";
import { faEyeSlash, faEye, faCalendar } from '@fortawesome/free-solid-svg-icons';
import { CalendarModule } from 'primeng/calendar';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';
import { eGender } from '../../../_generated/enums';
import { Functions } from '../../../functions';
import { ValidationDirective } from '../../../directives/validation.directive';
import { FileSelectEvent, FileUploadModule } from 'primeng/fileupload';
import { IBasicObject } from '../../../models/models';
import { Providers } from '../../../_generated/providers';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [GLOBAL_MODULES, ReactiveFormsModule, RequiredFieldMarkComponent, CalendarModule, DropdownModule, ValidationDirective, FileUploadModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
  providers: [Providers]
})
export class SignupComponent extends BaseFormComponent<ISignupDto> implements OnInit {
  image?: File;
  countries: ICountryDto[] = [];
  countryLoader = false;
  genders: IEnumProvider[] = [];
  icons = {
    faEyeSlash,
    faEye,
    faCalendar
  };
  isPasswordVisible = false;
  currentIcon = this.icons.faEyeSlash;
  currentCountry?: ICountryDto;
  currentGender?: IBasicObject;

  constructor
    (
      errorService: ErrorService,
      loaderService: PageLoaderService,
      authService: AuthService,
      toastService: ToastService,
      fb: FormBuilder,
      private providerController: ProviderController,
      private authController: AuthController,
      private providers: Providers
    ) {
    super(errorService, loaderService, authService, toastService, fb);
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
      [this.nameof(_ => _.details)]: this.fb.group({
        [this.nameof(_ => _.details.genderId, { lastPart: true })]: [0],
        [this.nameof(_ => _.details.phone, { lastPart: true })]: [''],
        [this.nameof(_ => _.details.countryId, { lastPart: true })]: [0],
        [this.nameof(_ => _.details.privacy, { lastPart: true })]: [false],
      }),
    })
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
    this.formData.append(this.nameof(_ => _.details.countryId), formValueObj.details.countryId.toString());
    this.formData.append(this.nameof(_ => _.details.genderId), formValueObj.details.genderId.toString());
    this.formData.append(this.nameof(_ => _.details.phone), formValueObj.details.phone);
    this.formData.append(this.nameof(_ => _.details.privacy), formValueObj.details.privacy.toString());

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
    this.currentIcon = this.isPasswordVisible ? this.icons.faEye : this.icons.faEyeSlash;
  }

  onGenderChange(): void {
    this.form.get('details.genderId')?.setValue(this.currentGender?.id);
  }

  getCountry(id: number): ICountryDto {
    return this.countries.find(_ => _.id === id) || {} as ICountryDto;
  }

  onCountryChange(event: DropdownChangeEvent): void {
    const country = this.countries.find(_ => _.id === event.value.id) || {} as ICountryDto;
    this.currentCountry = event.value;
    this.form.get('details.countryId')?.setValue(country.id);
    this.form.get('details.phone')?.setValue(country.dialCode + ' ');
  }

  onFileSelect(input: FileSelectEvent) {
    if (input.currentFiles && input.currentFiles[0])
      this.image = input.currentFiles[0];
  }
}
