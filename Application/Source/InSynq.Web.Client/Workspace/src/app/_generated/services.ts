import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ITokenDto } from './interfaces';
import { ISigninDto } from './interfaces';
import { ISignupDto } from './interfaces';
import { IDocumentDto } from './interfaces';
import { eLegalDocumentType } from './enums';
import { IFollowDto } from './interfaces';
import { ICountryDto } from './interfaces';
import { IUserDto } from './interfaces';
import { IUserLogDto } from './interfaces';

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
@Injectable() export class DocumentController extends BaseController
{
	public GetByType(type: eLegalDocumentType) : Observable<IDocumentDto | null>
	{
		const body = <any>{'type': type};
		return this.httpClient.get<IDocumentDto>(
		this.settingsService.createApiUrl('Document/GetByType'),
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
@Injectable() export class FollowController extends BaseController
{
	public Follow(data: IFollowDto) : Observable<any>
	{
		const body = <any>data;
		return this.httpClient.post<any>(
		this.settingsService.createApiUrl('Follow/Follow'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public Unfollow(data: IFollowDto) : Observable<any>
	{
		const body = <any>data;
		return this.httpClient.post<any>(
		this.settingsService.createApiUrl('Follow/Unfollow'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public IsFollowing(data: IFollowDto) : Observable<boolean | null>
	{
		const body = <any>data;
		return this.httpClient.post<boolean>(
		this.settingsService.createApiUrl('Follow/IsFollowing'),
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
	public GetCurrentUser() : Observable<IUserDto | null>
	{
		return this.httpClient.get<IUserDto>(
		this.settingsService.createApiUrl('User/GetCurrentUser'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public GetSingle(id: number) : Observable<IUserDto | null>
	{
		return this.httpClient.get<IUserDto>(
		this.settingsService.createApiUrl('User/GetSingle') + '/' + id,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body));
		
	}
	public GetUserLog(id: number) : Observable<IUserLogDto | null>
	{
		return this.httpClient.get<IUserLogDto>(
		this.settingsService.createApiUrl('User/GetUserLog') + '/' + id,
		{
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
	public Update(data: IUserDto) : Observable<any>
	{
		const body = <any>data;
		return this.httpClient.post<any>(
		this.settingsService.createApiUrl('User/Update'),
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
