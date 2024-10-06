import { Location } from "@angular/common";
import { Injectable } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";
import { ErrorService } from "../services/error.service";
import { PageLoaderService } from "../services/page-loader.service";
import { ToastService } from "../services/toast.service";
import { BaseComponent } from "./base.component";

@Injectable()
export class BaseProfileSettingsComponent extends BaseComponent {
    userId = 0;

    constructor(
        errorService: ErrorService,
        loaderService: PageLoaderService,
        authService: AuthService,
        toastService: ToastService,
        router: Router,
        protected location: Location,
        protected route: ActivatedRoute,
    ) {
        super(errorService, loaderService, authService, toastService, router)
        this.route.paramMap.subscribe(params => {
            this.userId = +params['get']('id')!;
        })
    }

    goBack(): void {
        this.location.back();
    }

    getRoute = (destination: string): string => `/profile/${this.userId}/settings/${destination}`;
}