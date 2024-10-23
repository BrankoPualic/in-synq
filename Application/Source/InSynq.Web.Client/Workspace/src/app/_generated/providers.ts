import { Injectable } from '@angular/core';
import * as api from '../api';

@Injectable() export class Providers
{
	getGenders() : api.EnumProvider[]
	{
		return [
		    { Id: 0, Name: 'NotSet', Description: '', BgColor: '#D7D7D7' },
		    { Id: 1, Name: 'Male', Description: 'Male', BgColor: '#00FFFF' },
		    { Id: 2, Name: 'Female', Description: 'Female', BgColor: '#FF0000' },
		    { Id: 3, Name: 'Other', Description: 'Other', BgColor: '#00FFFF' }
		];
	}
	getSystemRoles() : api.EnumProvider[]
	{
		return [
		    { Id: 1, Name: 'Admin', Description: 'Administrator', BgColor: '' },
		    { Id: 2, Name: 'Member', Description: 'Member', BgColor: '' },
		    { Id: 3, Name: 'UserAdmin', Description: 'User Admin', BgColor: '' },
		    { Id: 4, Name: 'Moderator', Description: 'Moderator', BgColor: '' },
		    { Id: 5, Name: 'LegalDepartment', Description: 'Legal Department', BgColor: '' }
		];
	}
}
