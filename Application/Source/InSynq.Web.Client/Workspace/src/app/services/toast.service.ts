import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private _warining = new Subject<string>();
  private _error = new Subject<string>();
  private _success = new Subject<string>();

  notifyWarning(message: string) {
    this._warining.next(message);
  }

  notifyError(message: string) {
    console.log(message)
    this._error.next(message);
  }

  notifySuccess(message: string) {
    this._success.next(message);
  }

  get warning() {
    return this._warining.asObservable();
  }
  get error() {
    return this._error.asObservable();
  }
  get success() {
    return this._success.asObservable();
  }
}
