import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable() export class BaseController {
  constructor(protected httpClient: HttpClient, protected settingsService: SettingsService) { }
}
