import { eLegalDocumentType } from './enums';
import { eGender } from './enums';

export class CountryDto
{
	Id: number;
	Name: string;
	Iso2Code: string;
	Iso3Code: string;
	DialCode: string;
	FlagUrl: string;
}
export class DocumentDto
{
	Id: number;
	Title: string;
	Content: string;
	TypeId: eLegalDocumentType;
	Type: LookupValueDto;
	Version: string;
	Language: string;
	CreatedOn: Date;
}
export class EnumProvider
{
	Id: number;
	Name: string;
	Description: string;
	BgColor: string;
}
export class FollowDto
{
	FollowerId: number;
	FollowingId: number;
}
export class LookupValueDto
{
	Id: number;
	Name: string;
	Description: string;
}
export class PagingResultDto<TData>
{
	Data: TData[];
	Total: number;
}
export class SigninDto
{
	Email: string;
	Password: string;
	Errors: any;
}
export class SignupDto
{
	FirstName: string;
	MiddleName: string;
	LastName: string;
	Username: string;
	Email: string;
	Password: string;
	ConfirmPassword: string;
	DateOfBirth: Date;
	Biography: string;
	Photo: File;
	GenderId: eGender;
	CountryId: number;
	Phone: string;
	Privacy: boolean;
	Errors: any;
}
export class TokenDto
{
	Token: string;
}
export class UserDto
{
	Id: number;
	Username: string;
	FullName: string;
	FirstName: string;
	MiddleName: string;
	LastName: string;
	ProfileImageUrl: string;
	Biography: string;
	DateOfBirth: Date;
	GenderId: eGender;
	Gender: LookupValueDto;
	Country: CountryDto;
	Phone: string;
	Privacy: boolean;
	Followers: number;
	Following: number;
	CreatedOn: Date;
	Errors: any;
}
export class UserLogDto
{
	UsernameCount: number;
	Usernames: string[];
}
