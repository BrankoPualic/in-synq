import { Constants } from "../constants/constants";
import { DateConstants } from "../constants/date-constants";
import { IconConstants } from "../constants/icon-constants";
import { ICurrentUser } from "./current-user.model";
import { IModelError } from "./error.model";

export interface IBaseComponent {
    errors: IModelError[];
    loading: boolean;
    hasAccess: boolean;
    currentUser: ICurrentUser | null;

    hasError(key: string): boolean;
    cleanErrors(): void;
}

export class BaseConstants {
    Constants = Constants;
    Icons = IconConstants;
    DateConstants = DateConstants;
}