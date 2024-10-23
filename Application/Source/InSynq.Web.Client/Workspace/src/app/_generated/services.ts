import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import { SettingsService } from '../services/settings.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TokenDto } from './classes';
import { SigninDto } from './classes';
import { SignupDto } from './classes';
import { DocumentDto } from './classes';
import { eLegalDocumentType } from './enums';
import { FollowDto } from './classes';
import { CountryDto } from './classes';
import { UserDto } from './classes';
import { UserLogDto } from './classes';

@Injectable() export class BaseController
{
	constructor (protected httpClient: HttpClient, protected settingsService: SettingsService) { } 
}
@Injectable() export class AuthController extends BaseController
{
	public Signin(data: SigninDto) : Observable<TokenDto>
	{
		const body = <any>data;
		return this.httpClient.post<TokenDto>(
		this.settingsService.createApiUrl('Auth/Signin'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	public Signup(data: FormData) : Observable<TokenDto>
	{
		const body = <any>data;
		return this.httpClient.post<TokenDto>(
		this.settingsService.createApiUrl('Auth/Signup'),
		body,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
@Injectable() export class DocumentController extends BaseController
{
	public GetByType(type: eLegalDocumentType) : Observable<DocumentDto>
	{
		const body = <any>{'type': type};
		return this.httpClient.get<DocumentDto>(
		this.settingsService.createApiUrl('Document/GetByType'),
		{
			params: new HttpParams({ fromObject: body }),
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
@Injectable() export class FollowController extends BaseController
{
	public Follow(data: FollowDto) : Observable<any>
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
		.pipe(map(response => response.body!));
		
	}
	public Unfollow(data: FollowDto) : Observable<any>
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
		.pipe(map(response => response.body!));
		
	}
	public IsFollowing(data: FollowDto) : Observable<boolean>
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
		.pipe(map(response => response.body!));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
@Injectable() export class ProviderController extends BaseController
{
	public GetCountries() : Observable<CountryDto[]>
	{
		return this.httpClient.get<CountryDto[]>(
		this.settingsService.createApiUrl('Provider/GetCountries'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
@Injectable() export class UserController extends BaseController
{
	public GetCurrentUser() : Observable<UserDto>
	{
		return this.httpClient.get<UserDto>(
		this.settingsService.createApiUrl('User/GetCurrentUser'),
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	public GetSingle(id: number) : Observable<UserDto>
	{
		return this.httpClient.get<UserDto>(
		this.settingsService.createApiUrl('User/GetSingle') + '/' + id,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	public GetUserLog(id: number) : Observable<UserLogDto>
	{
		return this.httpClient.get<UserLogDto>(
		this.settingsService.createApiUrl('User/GetUserLog') + '/' + id,
		{
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	public Search(options: any) : Observable<UserDto[]>
	{
		const body = <any>{'options': options};
		return this.httpClient.get<UserDto[]>(
		this.settingsService.createApiUrl('User/Search'),
		{
			params: new HttpParams({ fromObject: body }),
			responseType: 'json',
			observe: 'response',
			withCredentials: true
		})
		.pipe(map(response => response.body!));
		
	}
	public Update(data: UserDto) : Observable<any>
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
		.pipe(map(response => response.body!));
		
	}
	constructor (httpClient: HttpClient, settingsService: SettingsService)
	{
		super(httpClient, settingsService);
	}
}
