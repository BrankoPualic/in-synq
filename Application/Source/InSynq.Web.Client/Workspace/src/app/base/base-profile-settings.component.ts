import { Location } from "@angular/common";
import { Injectable } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { BaseConstants } from "../models/base-component.model";

@Injectable()
export class BaseProfileSettingsComponent extends BaseConstants {
    userId = 0;

    constructor(private location: Location, private route: ActivatedRoute) {
        super()
        this.route.paramMap.subscribe(params => {
            this.userId = +params['get']('id')!;
        })
    }

    goBack = () => this.location.back();

    getRoute = (destination: string): string => `/profile/${this.userId}/settings/${destination}`;
}