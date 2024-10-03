import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ITokenDto } from './interfaces';
import { ISigninDto } from './interfaces';
import { ISignupDto } from './interfaces';
import { ICountryDto } from './interfaces';
import { IUserDto } from './interfaces';

@Injectable() export class BaseController
{
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
@Injectable() export class AuthController extends BaseController
{
	public Signin(data: ISigninDto) : Observable<ITokenDto | null>
	{
		const body = <any>data;
		return this.httpClient.post<ITokenDto>(
		this.settingsService.createApiUrl('Auth/Signin'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public Signup(data: FormData) : Observable<ITokenDto | null>
	{
		const body = <any>data;
		return this.httpClient.post<ITokenDto>(
		this.settingsService.createApiUrl('Auth/Signup'),
		body,
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
@Injectable() export class UserController extends BaseController
{
	public GetSingle(id: number) : Observable<IUserDto | null>
	{
		const body = <any>{'id': id};
		return this.httpClient.get<IUserDto>(
		this.settingsService.createApiUrl('User/GetSingle'),
		{
			params: new HttpParams({ fromObject: body }),
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public Search(options: any) : Observable<IUserDto[] | null>
	{
		const body = <any>{'options': options};
		return this.httpClient.get<IUserDto[]>(
		this.settingsService.createApiUrl('User/Search'),
		{
			params: new HttpParams({ fromObject: body }),
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
