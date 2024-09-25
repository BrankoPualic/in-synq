import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ICountryDto } from './interfaces';

@Injectable() export class BaseController
{
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
@Injectable() export class ProviderController extends BaseController
{
	public GetCountries() : Observable<ICountryDto[] | null>
	{
		return this.httpClient.get<ICountryDto[]>(
		this.settingsService.createApiUrl('Provider/GetCountries'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
