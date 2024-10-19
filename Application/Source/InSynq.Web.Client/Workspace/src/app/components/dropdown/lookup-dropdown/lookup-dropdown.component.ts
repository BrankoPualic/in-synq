import { CommonModule } from '@angular/common';
import { Component, input, OnInit } from '@angular/core';
import { IEnumProvider } from '../../../_generated/interfaces';
import { BaseDropdownComponent } from '../../../base/base-dropdown.component';
import { DropdownComponent } from '../dropdown.component';
import { Providers } from '../../../_generated/providers';

@Component({
  selector: 'app-lookup-dropdown',
  standalone: true,
  imports: [CommonModule, DropdownComponent],
  templateUrl: './lookup-dropdown.component.html',
  styleUrl: './lookup-dropdown.component.scss'
})
export class LookupDropdownComponent extends BaseDropdownComponent<IEnumProvider> implements OnInit {
  provider = input<string>();
  options: IEnumProvider[] = [];

  constructor(private providers: Providers) { super() }

  ngOnInit(): void {
    const methodName = `get${this.provider()}` as keyof Providers;

    const providerMethod = this.providers[methodName] as (() => IEnumProvider[]) | undefined;
    if (typeof providerMethod === 'function')
      this.options = providerMethod();
  }

  override change() {
    this.onChangeVoid.emit();
  }
}
