import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { RippleModule } from 'primeng/ripple';
import { ToastModule } from 'primeng/toast';
import { LoaderComponent } from './components/loader.component';
import { AuthService } from './services/auth.service';
import { ToastService } from './services/toast.service';
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
    private authService: AuthService,
  ) { }

  ngOnInit(): void {
    this.toastService.error.subscribe(_ => this.messageService.add({ severity: 'error', summary: 'Error', detail: _ }));
    this.toastService.warning.subscribe(_ => this.messageService.add({ severity: 'warn', detail: _ }));
    this.toastService.success.subscribe(_ => this.messageService.add({ severity: 'success', detail: _ }));

    this.authService.loadCurrentUser();
  }
}
