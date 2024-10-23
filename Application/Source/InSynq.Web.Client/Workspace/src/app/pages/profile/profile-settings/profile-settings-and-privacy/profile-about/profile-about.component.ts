import { Location } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';
import { DropdownChangeEvent, DropdownModule } from 'primeng/dropdown';
import { Subject, takeUntil } from 'rxjs';
import { GLOBAL_MODULES } from '../../../../../../_global.modules';
import { BaseProfileSettingsComponent } from '../../../../../base/base-profile-settings.component';
import { CountryDropdownComponent } from "../../../../../components/dropdown/country-dropdown/country-dropdown.component";
import { DropdownComponent } from '../../../../../components/dropdown/dropdown.component';
import { LoaderComponent } from "../../../../../components/loader.component";
import { RequiredFieldMarkComponent } from "../../../../../components/required-field-mark.component";
import { ValidationDirective } from '../../../../../directives/validation.directive';
import { Functions } from '../../../../../functions';
import { AuthService } from '../../../../../services/auth.service';
import { CustomConfirmationService } from '../../../../../services/custom-confirmation.service';
import { ErrorService } from '../../../../../services/error.service';
import { PageLoaderService } from '../../../../../services/page-loader.service';
import { ProfileService } from '../../../../../services/profile.service';
import { ToastService } from '../../../../../services/toast.service';
import { LookupDropdownComponent } from "../../../../../components/dropdown/lookup-dropdown/lookup-dropdown.component";
import * as api from '../../../../../api';
@Component({
  selector: 'app-profile-about',
  standalone: true,
  imports: [GLOBAL_MODULES, DialogModule, RequiredFieldMarkComponent, CalendarModule, DropdownModule, ValidationDirective, LoaderComponent, DropdownComponent, CountryDropdownComponent, LookupDropdownComponent],
  templateUrl: './profile-about.component.html',
  styleUrl: './profile-about.component.scss'
})
export class ProfileAboutComponent extends BaseProfileSettingsComponent implements OnInit, OnDestroy {
  private _originalProfile?: api.UserDto;
  private _cpDestroy$ = new Subject<void>();
  userLog: api.UserLogDto | null = null;
  usernamesVisible = false;
  isEditMode = false;
  currentGender?: api.EnumProvider;
  currentCountry?: api.CountryDto;

  isChanged = false;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    location: Location,
    route: ActivatedRoute,
    private profileService: ProfileService,
    private confirmationService: CustomConfirmationService,
    private api_UserController: api.UserController,
    private api_Providers: api.Providers
  ) {
    super(errorService, loaderService, authService, toastService, router, location, route)
  }

  ngOnInit(): void {
    this.profileService.profile$.pipe(takeUntil(this._cpDestroy$)).subscribe(_ => {
      this.profile = _;
      this.profile!.DateOfBirth = new Date(_!.DateOfBirth);
      this.currentCountry = _?.Country;
    });

    this.currentGender = this.api_Providers.getGenders().find(_ => _.Id === this.profile?.GenderId);

    this.loading = true;
    this.api_UserController.GetUserLog(this.userId).toPromise()
      .then(_ => this.userLog = _!)
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }

  override ngOnDestroy(): void {
    this._cpDestroy$.next();
    this._cpDestroy$.complete();
  }

  showUsernames = (): void => { this.usernamesVisible = true; };

  onInputTextChange = (): void => { this.isChanged = true };

  onGenderChange(): void {
    this.isChanged = true;
    this.profile!.GenderId = this.currentGender?.Id as api.eGender;
  }

  onDoBSelect = (): void => { this.isChanged = true; }

  onCountryChange(event: DropdownChangeEvent): void {
    this.isChanged = true;
    this.currentCountry = event.value;
    this.profile!.Country = event.value;
    this.profile!.Phone = event.value.dialCode + ' ';
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
        this.currentCountry = this.profile.Country;
        this.currentGender = this.currentGender = this.api_Providers.getGenders().find(_ => _.Id === this.profile?.GenderId);

        this.isEditMode = false
      });
  }

  save(): void {
    this.confirmationService.confirm('Are you sure you want to save changes?').result
      .then(() => {
        this.loading = true;
        this.cleanErrors();
        this.profile!.DateOfBirth = new Date(Functions.localDateToUtcFormat(this.profile!.DateOfBirth));

        this.api_UserController.Update(this.profile!).toPromise()
          .then(() => {
            this.success('Profile updated.');
            this.isChanged = false;
            this.isEditMode = false;
            this.authService.loadCurrentUser();
          })
          .catch(_ => this.errorService.add(_.error.errors))
          .finally(() => this.loading = false);
      });
  }
}
