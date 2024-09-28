import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ToastService } from './services/toast.service';
import { RippleModule } from 'primeng/ripple';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastModule, RippleModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [MessageService]
})
export class AppComponent implements OnInit {
  constructor(private messageService: MessageService, private toastService: ToastService) { }

  ngOnInit(): void {
    this.toastService.error.subscribe(_ => this.messageService.add({ severity: 'error', summary: 'Error', detail: _ }));
    this.toastService.warning.subscribe(_ => this.messageService.add({ severity: 'warn', detail: _ }));
    this.toastService.success.subscribe(_ => this.messageService.add({ severity: 'success', detail: _ }));
  }
}
