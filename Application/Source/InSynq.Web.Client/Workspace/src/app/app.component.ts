import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MessageService } from 'primeng/api';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { LoaderComponent } from './components/loader.component';
import { ToastService } from './services/toast.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { UserController } from './_generated/services';
import { ProfileService } from './services/profile.service';
import { PageLoaderService } from './services/page-loader.service';
import { AuthService } from './services/auth.service';
import { ErrorService } from './services/error.service';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastModule, RippleModule, LoaderComponent, ConfirmDialogModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [MessageService]
})
export class AppComponent implements OnInit {
  constructor(
    private messageService: MessageService,
    private toastService: ToastService,
    private loaderService: PageLoaderService,
    private profileService: ProfileService,
    private authService: AuthService,
    private errorService: ErrorService,
    private userController: UserController
  ) { }

  ngOnInit(): void {
    this.toastService.error.subscribe(_ => this.messageService.add({ severity: 'error', summary: 'Error', detail: _ }));
    this.toastService.warning.subscribe(_ => this.messageService.add({ severity: 'warn', detail: _ }));
    this.toastService.success.subscribe(_ => this.messageService.add({ severity: 'success', detail: _ }));

    this.loaderService.show();
    const currentUser = this.authService.getCurrentUser();
    this.userController.GetCurrentUser(currentUser?.id || 0).toPromise()
      .then(_ => this.profileService.setProfile(_!))
      .catch((_: HttpErrorResponse) => this.errorService.add(_.error.errors))
      .finally(() => this.loaderService.hide());
  }
}
