import { Component, OnInit } from '@angular/core';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseComponentGeneric } from '../../base/base.component';
import { AuthService } from '../../services/auth.service';
import { ErrorService } from '../../services/error.service';
import { PageLoaderService } from '../../services/page-loader.service';
import { ToastService } from '../../services/toast.service';
import { MobileNavigationComponent } from "../../components/mobile-navigation/mobile-navigation.component";
import { UserController } from '../../_generated/services';
import { IUserDto } from '../../_generated/interfaces';
import { HttpErrorResponse } from '@angular/common/http';
import { eGender } from '../../_generated/enums';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [GLOBAL_MODULES, MobileNavigationComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent extends BaseComponentGeneric<IUserDto> implements OnInit {
  model: IUserDto | null = null;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    private userController: UserController
  ) {
    super(errorService, loaderService, authService, toastService);
  }

  get profilePhoto(): string {
    return this.model?.profileImageUrl ||
      `../../../assets/images/${this.model?.gender.id === eGender.Male
        ? 'default-avatar-profile-picture-male-icon.png'
        : 'default-avatar-profile-picture-female-icon.png'}`;
  }

  ngOnInit(): void {
    this.loadUser();
  }

  showPrivacy = (): boolean => !!this.currentUser && this.model?.id === this.currentUser.id;

  private loadUser(): void {
    if (!this.currentUser)
      return;

    this.loading = true;
    this.userController.GetSingle(this.currentUser.id!).toPromise()
      .then(_ => this.model = _)
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }
}
