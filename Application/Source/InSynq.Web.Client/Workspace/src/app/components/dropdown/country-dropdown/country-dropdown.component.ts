import { Component, OnInit } from '@angular/core';
import * as api from '../../../api';
import { BaseDropdownComponent } from '../../../base/base-dropdown.component';
import { ToastService } from '../../../services/toast.service';
import { DropdownComponent } from '../dropdown.component';

@Component({
  selector: 'app-country-dropdown',
  standalone: true,
  imports: [DropdownComponent],
  templateUrl: './country-dropdown.component.html',
  styleUrl: './country-dropdown.component.scss'
})
export class CountryDropdownComponent extends BaseDropdownComponent<api.CountryDto> implements OnInit {
  loader = false;
  options: api.CountryDto[] = [];

  constructor(
    private toastService: ToastService,
    private api_ProviderController: api.ProviderController,
  ) { super() }

  ngOnInit(): void {
    this.options.push(this.value()!);

    if (this.initialLoad())
      this.loadCountries();
  }

  override click(): void {
    if (!this.initialLoad())
      this.loadCountries();
  }

  private loadCountries() {
    this.loader = true;
    this.api_ProviderController.GetCountries().toPromise()
      .then(_ => { this.options = []; this.options = _ })
      .catch(_ => this.toastService.notifyError(_.error.errors))
      .finally(() => this.loader = false);
  }
}
