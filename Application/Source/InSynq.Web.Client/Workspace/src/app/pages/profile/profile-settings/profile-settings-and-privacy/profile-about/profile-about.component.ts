import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { eGender } from '../../../../../_generated/enums';
import { ICountryDto, IEnumProvider, IUserDto, IUserLogDto } from '../../../../../_generated/interfaces';
import { Providers } from '../../../../../_generated/providers';
import { ProviderController, UserController } from '../../../../../_generated/services';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { RequiredFieldMarkComponent } from "../../../../../components/required-field-mark.component";
import { ValidationDirective } from '../../../../../directives/validation.directive';
import { Functions } from '../../../../../functions';
import { AuthService } from '../../../../../services/auth.service';
import { CustomConfirmationService } from '../../../../../services/custom-confirmation.service';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { ProfileService } from '../../../../../services/profile.service';
import { QueueService } from '../../../../../services/queue.service';
import { ToastService } from '../../../../../services/toast.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-profile-about',
  standalone: true,
  imports: [GLOBAL_MODULES, DialogModule, RequiredFieldMarkComponent, CalendarModule, DropdownModule, ValidationDirective],
  templateUrl: './profile-about.component.html',
  styleUrl: './profile-about.component.scss'
})
export class ProfileAboutComponent extends BaseProfileSettingsComponent implements OnInit, OnDestroy {
  private _originalProfile?: IUserDto;
  private _cpDestroy$ = new Subject<void>();
  userLog: IUserLogDto | null = null;
  usernamesVisible = false;
  isEditMode = false;
  genders: IEnumProvider[] = [];
  currentGender?: IEnumProvider;
  countries: ICountryDto[] = [];
  countryLoader = false;
  currentCountry?: ICountryDto;

  isChanged = false;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private $q: QueueService,
    private profileService: ProfileService,
    private confirmationService: CustomConfirmationService,
    private userController: UserController,
    private providerController: ProviderController,
    private providers: Providers
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  ngOnInit(): void {
    this.profileService.profile$.pipe(takeUntil(this._cpDestroy$)).subscribe(_ => {
      this.profile = _;
      this.profile!.dateOfBirth = new Date(_!.dateOfBirth);
      this.currentCountry = _?.country;
    });

    this.genders = this.providers.getGenders();
    this.currentGender = this.genders.find(_ => _.id === this.profile?.genderId);

    this.loading = true;
    this.countryLoader = true;
    this.$q.all([
      this.userController.GetUserLog(this.userId).toPromise(),
      this.providerController.GetCountries().toPromise()
    ])
      .then((result) => {
        this.userLog = result[0];
        this.countries = result[1] ?? [];
      })
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => {
        this.loading = false;
        this.countryLoader = false;
      });
  }

  override ngOnDestroy(): void {
    this._cpDestroy$.next();
    this._cpDestroy$.complete();
  }

  showUsernames = (): void => { this.usernamesVisible = true; }

  getCountry(id: number): ICountryDto {
    return this.countries.find(_ => _.id === id) || {} as ICountryDto;
  }

  onInputTextChange = (): void => { this.isChanged = true };

  onGenderChange(): void {
    this.isChanged = true;
    this.profile!.genderId = this.currentGender?.id as eGender;
  }

  onDoBSelect = (): void => { this.isChanged = true; }

  onCountryChange(event: DropdownChangeEvent): void {
    this.isChanged = true;
    const country = this.countries.find(_ => _.id === event.value.id) || {} as ICountryDto;
    this.currentCountry = event.value;
    this.profile!.country = event.value;
    this.profile!.phone = event.value.dialCode + ' ';
  }

  edit(): void {
    this._originalProfile = this.clone(this.profile!);
    this.isEditMode = true;
  };

  cancel(): void {
    if (!this.isChanged) {
      this.isEditMode = false;
      return;
    }

    this.confirmationService.confirm('Are you sure you want to cancel all changes without saving?').result
      .then(() => {
        this.cleanErrors();

        this.profile = this.clone(this._originalProfile);
        this.currentCountry = this.profile.country;
        this.currentGender = this.currentGender = this.genders.find(_ => _.id === this.profile?.genderId);

        this.isEditMode = false
      });
  }

  save(): void {
    this.confirmationService.confirm('Are you sure you want to save changes?').result
      .then(() => {
        this.loading = true;
        this.cleanErrors();

        this.profile!.dateOfBirth = new Date(Functions.localDateToUtcFormat(this.profile!.dateOfBirth));

        this.userController.Update(this.profile!).toPromise()
          .then(() => {
            this.success('Profile updated.');

            this.isChanged = false;
            this.isEditMode = false;

            this.userController.GetCurrentUser(this.userId).toPromise()
              .then(_ => this.profileService.setProfile(_!))
              .catch(_ => this.error(_.error.erros));
          })
          .catch(_ => this.errorService.add(_.error.errors))
          .finally(() => this.loading = false);
      });
  }
}
