import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { eGender } from '../_generated/enums';
import { IUserDto } from '../_generated/interfaces';
import { Constants } from '../constants/constants';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private _profileSource = new BehaviorSubject<IUserDto | null>(null);
  profile$ = this._profileSource.asObservable();
  Constants = Constants;

  getProfilePhoto(photo?: string, genderId?: number): string {
    return photo || `../../../assets/images/${(genderId || 0) === eGender.Male
      ? Constants.DEFAULT_PHOTO_MALE
      : Constants.DEFAULT_PHOTO_FEMALE}`;
  }

  setProfile(user: IUserDto): void {
    user.profileImageUrl = this.getProfilePhoto(user.profileImageUrl, user.gender.id);
    this._profileSource.next(user);
  }
}
