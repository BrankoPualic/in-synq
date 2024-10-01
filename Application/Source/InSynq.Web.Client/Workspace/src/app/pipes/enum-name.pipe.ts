import { Pipe, PipeTransform } from '@angular/core';
import { Providers } from '../_generated/providers';
import { IEnumProvider } from '../_generated/interfaces';

@Pipe({
  name: 'enumName',
  standalone: true
})
export class EnumNamePipe implements PipeTransform {
  constructor(private providers: Providers) { }

  transform(value: number, provider: string, field: keyof IEnumProvider = 'description'): string | number | null {
    const methodName = `get${provider}` as keyof Providers;

    const providerMethod = this.providers[methodName] as (() => IEnumProvider[]) | undefined;
    if (typeof providerMethod !== 'function')
      return null;

    let enumList: IEnumProvider[] = providerMethod();

    const enumItem = enumList.find(item => item.id === value);
    if (!enumItem)
      return null;

    return enumItem[field] || null;
  }
}