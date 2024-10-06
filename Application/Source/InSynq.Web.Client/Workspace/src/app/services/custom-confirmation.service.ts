import { Injectable } from '@angular/core';
import { ConfirmationService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class CustomConfirmationService {
  constructor(private confirmationService: ConfirmationService) { }

  confirm(message: string): { result: Promise<void> } {
    let promiseResolve: () => void;

    const result = new Promise<void>((resolve, reject) => promiseResolve = resolve)

    this.confirmationService.confirm({
      header: 'Confirm',
      message: message,
      accept: () => promiseResolve(),
      reject: () => { },
    })

    return { result };
  }
}
