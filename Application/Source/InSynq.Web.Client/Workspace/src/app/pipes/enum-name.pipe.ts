import { Pipe, PipeTransform } from '@angular/core';
import * as api from '../api';

@Pipe({
  name: 'enumName',
  standalone: true
})
export class EnumNamePipe implements PipeTransform {
  constructor(private api_Providers: api.Providers) { }

  transform(value: number, provider: string, field: keyof api.EnumProvider = 'Description'): string | number | null {
    const methodName = `get${provider}` as keyof api.Providers;

    const providerMethod = this.api_Providers[methodName] as (() => api.EnumProvider[]) | undefined;
    if (typeof providerMethod !== 'function')
      return null;

    let enumList: api.EnumProvider[] = providerMethod();

    const enumItem = enumList.find(item => item.Id === value);
    if (!enumItem)
      return null;

    return enumItem[field] || null;
  }
}