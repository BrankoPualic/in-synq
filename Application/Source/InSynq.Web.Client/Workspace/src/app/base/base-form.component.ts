import { AuthService } from "../services/auth.service";
import { ErrorService } from "../services/error.service";
import { PageLoaderService } from "../services/page-loader.service";
import { BaseComponentGeneric } from "./base.component";
import { FormBuilder, FormGroup } from '@angular/forms';

/**
 * Import ReactiveFormsModule inside component imports if it's standalone component.
 */
export abstract class BaseFormComponent<T extends object> extends BaseComponentGeneric<T> {
    protected form: FormGroup;
    protected formData: FormData;

    constructor(errorService: ErrorService, loaderService: PageLoaderService, authService: AuthService, protected fb: FormBuilder) {
        super(errorService, loaderService, authService);

        this.form = this.fb.group({});
        this.formData = new FormData();
    }

    protected abstract initializeForm(): void;

    protected abstract submit(): void;

    protected toFormData(formValueObj: T): void { }
}