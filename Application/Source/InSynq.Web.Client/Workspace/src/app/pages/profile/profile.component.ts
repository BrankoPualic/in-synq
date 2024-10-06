import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GLOBAL_MODULES } from '../../../_global.modules';
import { eGender } from '../../_generated/enums';
import { IFollowDto, IUserDto } from '../../_generated/interfaces';
import { FollowController, UserController } from '../../_generated/services';
import { BaseComponentGeneric } from '../../base/base.component';
import { MobileNavigationComponent } from "../../components/mobile-navigation/mobile-navigation.component";
import { AuthService } from '../../services/auth.service';
import { ErrorService } from '../../services/error.service';
import { PageLoaderService } from '../../services/page-loader.service';
import { ToastService } from '../../services/toast.service';
import { ProfileGalleryComponent } from './profile-gallery/profile-gallery.component';
import { ProfileTagsComponent } from './profile-tags/profile-tags.component';
import { ProfileThreadsComponent } from './profile-threads/profile-threads.component';
import { ProfileVideosComponent } from './profile-videos/profile-videos.component';
import { ProfileService } from '../../services/profile.service';

enum eComponentState {
  Gallery = 1,
  Threads = 2,
  Videos = 3,
  Tags = 4,
}
interface IComponentState {
  gallery: boolean;
  threads: boolean;
  videos: boolean;
  tags: boolean;
}
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [GLOBAL_MODULES, MobileNavigationComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent extends BaseComponentGeneric<IUserDto> implements OnInit {
  @ViewChild('container', { read: ViewContainerRef, static: true }) container?: ViewContainerRef;
  model: IUserDto | null = null;
  userId = 0;
  isFollowed = false;
  followData = {} as IFollowDto;
  state = {} as IComponentState;
  ComponentState = eComponentState;

  constructor(
    errorService: ErrorService,
    loaderService: PageLoaderService,
    authService: AuthService,
    toastService: ToastService,
    router: Router,
    private route: ActivatedRoute,
    private profileService: ProfileService,
    private userController: UserController,
    private followController: FollowController
  ) {
    super(errorService, loaderService, authService, toastService, router);
  }

  get profilePhoto(): string {
    return this.profileService.getProfilePhoto(this.model?.profileImageUrl, this.model?.gender.id);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.userId = +params['get']('id')!;

      this.followData = {
        followerId: this.currentUser?.id || 0,
        followingId: this.userId
      };

      this.state = {
        gallery: true,
        threads: false,
        videos: false,
        tags: false
      };

      this.loadUser();
      this.isUserFollowed();
      this.loadComponent();
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

  changeComponent(state: eComponentState): void {
    this.state = {
      gallery: false,
      threads: false,
      videos: false,
      tags: false
    };

    switch (state) {
      case eComponentState.Gallery:
        this.state.gallery = true;
        break;
      case eComponentState.Threads:
        this.state.threads = true;
        break;
      case eComponentState.Videos:
        this.state.videos = true;
        break;
      case eComponentState.Tags:
        this.state.tags = true;
        break;
      default:
        this.state.gallery = true;
        break;
    }

    this.loadComponent();
  }

  private loadComponent(): void {
    this.container?.clear();
    let component;

    if (this.state.gallery)
      component = ProfileGalleryComponent;
    else if (this.state.threads)
      component = ProfileThreadsComponent;
    else if (this.state.videos)
      component = ProfileVideosComponent;
    else if (this.state.tags)
      component = ProfileTagsComponent;

    this.container?.createComponent(component!);
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
