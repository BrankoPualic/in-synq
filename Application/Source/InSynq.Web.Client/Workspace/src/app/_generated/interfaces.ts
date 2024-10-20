import { eLegalDocumentType } from './enums';
import { eGender } from './enums';

export interface ICountryDto
{
	id: number;
	name: string;
	iso2Code: string;
	iso3Code: string;
	dialCode: string;
	flagUrl: string;
}
export interface IDocumentDto
{
	id: number;
	title: string;
	content: string;
	typeId: eLegalDocumentType;
	type: ILookupValueDto;
	version: string;
	language: string;
	createdOn: Date;
}
export interface IFollowDto
{
	followerId: number;
	followingId: number;
}
export interface IEnumProvider
{
	id: number;
	name: string;
	description: string;
	bgColor: string;
}
export interface ILookupValueDto
{
	id: number;
	name: string;
	description: string;
}
export interface IPagingResultDto<TData>
{
	data: TData[];
	total: number;
}
export interface ISigninDto
{
	email: string;
	password: string;
}
export interface ISignupDto
{
	firstName: string;
	middleName: string;
	lastName: string;
	username: string;
	email: string;
	password: string;
	confirmPassword: string;
	dateOfBirth: Date;
	biography: string;
	photo: File;
	genderId: eGender;
	countryId: number;
	phone: string;
	privacy: boolean;
}
export interface ITokenDto
{
	token: string;
}
export interface IUserDto
{
	id: number;
	username: string;
	fullName: string;
	firstName: string;
	middleName: string;
	lastName: string;
	profileImageUrl: string;
	biography: string;
	dateOfBirth: Date;
	genderId: eGender;
	gender: ILookupValueDto;
	country: ICountryDto;
	phone: string;
	privacy: boolean;
	followers: number;
	following: number;
	createdOn: Date;
}
export interface IUserLogDto
{
	usernameCount: number;
	usernames: string[];
}
