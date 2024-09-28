import {
  ApplicationConfig,
  Provider,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import {
  provideHttpClient,
  withFetch,
  withInterceptors,
} from '@angular/common/http';
import * as clr from './_generated/services';
import { SettingsService } from './services/settings.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideClientHydration(),
    provideAnimations(),
    provideHttpClient(withFetch(), withInterceptors([])),
    serviceProviders(),
    controllerProviders(),
  ],
};

function controllerProviders(): Provider[] {
  return [clr.ProviderController, clr.AuthController];
}

function serviceProviders(): Provider[] {
  return [SettingsService];
}
