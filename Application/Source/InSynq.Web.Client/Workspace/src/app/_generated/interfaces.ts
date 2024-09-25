import { eGender } from './enums';

export interface IBaseDto
{
	error: any;
}
export interface ICountryDto
{
	id: number;
	name: string;
	iso2Code: string;
	iso3Code: string;
	dialCode: string;
	flagUrl: string;
}
export interface ISigninDto
{
	username: string;
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
	image?: File;
	details: IUserDetailsDto;
}
export interface ITokenDto
{
	token: string;
}
export interface IUserDetailsDto
{
	genderId: eGender;
	privacy: boolean;
	phone: string;
	country: ICountryDto;
}
