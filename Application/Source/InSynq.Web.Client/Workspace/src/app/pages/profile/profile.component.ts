import { Component, OnInit } from '@angular/core';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { BaseComponentGeneric } from '../../base/base.component';
import { AuthService } from '../../services/auth.service';
import { ErrorService } from '../../services/error.service';
import { PageLoaderService } from '../../services/page-loader.service';
import { ToastService } from '../../services/toast.service';
import { MobileNavigationComponent } from "../../components/mobile-navigation/mobile-navigation.component";
import { FollowController, UserController } from '../../_generated/services';
import { IFollowDto, IUserDto } from '../../_generated/interfaces';
import { HttpErrorResponse } from '@angular/common/http';
import { eGender } from '../../_generated/enums';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [GLOBAL_MODULES, MobileNavigationComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent extends BaseComponentGeneric<IUserDto> implements OnInit {
  model: IUserDto | null = null;
  userId: number = 0;
  isFollowed = false;
  followData = {} as IFollowDto;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    private route: ActivatedRoute,
    private userController: UserController,
    private followController: FollowController
  ) {
    super(errorService, loaderService, authService, toastService, router);
  }

  get profilePhoto(): string {
    return this.model?.profileImageUrl ||
      `../../../assets/images/${this.model?.gender.id === eGender.Male
        ? 'default-avatar-profile-picture-male-icon.png'
        : 'default-avatar-profile-picture-female-icon.png'}`;

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = +params['get']('id')!;
      this.followData = {
        followerId: this.currentUser?.id || 0,
        followingId: this.userId
      }
      this.loadUser();
      this.isUserFollowed();
    });

  }

  isMyProfile = (): boolean => !!this.currentUser && this.model?.id === this.currentUser.id;

  follow(): void {
    this.loading = true;
    this.followController.Follow(this.followData).toPromise()
      .then(() => {
        this.loadUser();
        this.isUserFollowed();
      })
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }

  unfollow(): void {
    this.loading = true;
    this.followController.Unfollow(this.followData).toPromise()
      .then(() => {
        this.loadUser();
        this.isUserFollowed();
      })
      .catch((_: HttpErrorResponse) => this.error(_.error.errors))
      .finally(() => this.loading = false);
  }

  private isUserFollowed(): void {
    this.loading = true;
    this.followController.IsFollowing(this.followData).toPromise()
      .then(_ => this.isFollowed = _!)
      .finally(() => this.loading = false);
  }

  private loadUser(): void {
    if (!this.currentUser)
      return;

    this.loading = true;
    this.userController.GetSingle(this.userId).toPromise()
      .then(_ => this.model = _)
      .catch((_: HttpErrorResponse) => this.errorAndRedirect(_.error.errors))
      .finally(() => this.loading = false);
  }
}
