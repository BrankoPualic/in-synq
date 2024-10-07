import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { eGender } from '../_generated/enums';
import { IUserDto } from '../_generated/interfaces';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private _profileSource = new BehaviorSubject<IUserDto | null>(null);
  profile$ = this._profileSource.asObservable();

  getProfilePhoto(photo?: string, genderId?: number): string {
    return photo || `../../../assets/images/${(genderId || 0) === eGender.Male
      ? 'default-avatar-profile-picture-male-icon.png'
      : 'default-avatar-profile-picture-female-icon.png'}`;
  }

  setProfile(user: IUserDto): void {
    user.profileImageUrl = this.getProfilePhoto(user.profileImageUrl, user.gender.id);

    this._profileSource.next(user);
  }
}
