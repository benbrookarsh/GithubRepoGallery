import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {provideHttpClient, withInterceptors} from '@angular/common/http';
import {TokenInterceptor} from './interceptors/http-interceptor';
import {provideClientHydration} from '@angular/platform-browser';
import {StorageService} from './services/storage.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideClientHydration(),
    StorageService,
    provideHttpClient(withInterceptors([TokenInterceptor])),
  ]
};
