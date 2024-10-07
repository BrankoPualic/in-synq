import { cloneDeep } from "lodash";
import { INameofOptions } from "./models/function-options.model";
import { IBasicObject } from "./models/models";

export class Functions {
    static nameof<T extends object>(
        exp: ((obj: T) => any) | (new (...params: any[]) => T),
        options?: INameofOptions,
    ): string {
        const fnStr = exp.toString();

        if (fnStr.substring(0, 6) == 'class ' && fnStr.substring(0, 8) != 'class =>') {
            return this.cleanseAssertionOperators(fnStr.substring('class '.length, fnStr.indexOf(' {')));
        }

        if (fnStr.indexOf('=>') !== -1) {
            let name = this.cleanseAssertionOperators(fnStr.substring(fnStr.indexOf('.') + 1));
            if (options?.lastPart) name = name.substring(name.lastIndexOf('.') + 1);
            return name;
        }

        throw new Error('ts-simple-nameof: Invalid function');
    }

    private static cleanseAssertionOperators(parsedName: string): string {
        return parsedName.replace(/[?!]/g, '');
    }

    // Enums
    static enumToArray<T extends object>(enumObj: T): IBasicObject[] {
        return Object.keys(enumObj)
            .filter(key => isNaN(Number(key)))
            .map(key => ({
                id: (enumObj as any)[key],
                name: key
            }));
    }

    // Object
    static clone = <T extends object>(model?: T): T => !!model ? cloneDeep(model) : {} as T;

    // Date

    /**
     * Appends Z on the end so it seems like it's an UTC. Only when you are having issues with HTTP converting date to UTC when you need to store just a date.
     * @param date Local date
     * @returns 
     */
    static localDateToUtcFormat = (date: Date): string => `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}-${String(date.getDate()).padStart(2, '0')}T00:00:00Z`;

    // String
    static formatString = (text?: string): string => !!text ? text.replace(/\n/g, '<br/>') : '';
}