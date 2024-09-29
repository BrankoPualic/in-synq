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
}