import { CommonModule } from '@angular/common';
import { Component, input, OnInit } from '@angular/core';
import { BaseDropdownComponent } from '../../../base/base-dropdown.component';
import { DropdownComponent } from '../dropdown.component';
import * as api from '../../../api';

@Component({
  selector: 'app-lookup-dropdown',
  standalone: true,
  imports: [CommonModule, DropdownComponent],
  templateUrl: './lookup-dropdown.component.html',
  styleUrl: './lookup-dropdown.component.scss'
})
export class LookupDropdownComponent extends BaseDropdownComponent<api.EnumProvider> implements OnInit {
  provider = input<string>();
  options: api.EnumProvider[] = [];

  constructor(private api_Providers: api.Providers) { super() }

  ngOnInit(): void {
    const methodName = `get${this.provider()}` as keyof api.Providers;

    const providerMethod = this.api_Providers[methodName] as (() => api.EnumProvider[]) | undefined;
    if (typeof providerMethod === 'function')
      this.options = providerMethod();

    console.log(this.options);
  }

  override change() {
    this.onChangeVoid.emit();
  }
}
