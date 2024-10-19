import { Component, model, OnInit } from '@angular/core';
import { ICountryDto } from '../../../_generated/interfaces';
import { ProviderController } from '../../../_generated/services';
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
export class CountryDropdownComponent extends BaseDropdownComponent<ICountryDto> implements OnInit {
  loader = false;
  options: ICountryDto[] = [];

  constructor(
    private toastService: ToastService,
    private providerController: ProviderController,
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
    this.providerController.GetCountries().toPromise()
      .then(_ => { this.options = []; this.options = _! })
      .catch(_ => this.toastService.notifyError(_.error.errors))
      .finally(() => this.loader = false);
  }
}
