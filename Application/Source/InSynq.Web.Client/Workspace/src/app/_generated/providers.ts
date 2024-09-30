import { Injectable } from '@angular/core';
import { IEnumProvider } from './interfaces';

@Injectable() export class Providers
{
	getGenders() : IEnumProvider[]
	{
		return [
		    { id: 0, name: 'NotSet', description: '', bgColor: '#D7D7D7' },
		    { id: 1, name: 'Male', description: 'Male', bgColor: '#00FFFF' },
		    { id: 2, name: 'Female', description: 'Female', bgColor: '#FF0000' },
		    { id: 3, name: 'Other', description: 'Other', bgColor: '#00FFFF' }
		];
	}
	getSystemRoles() : IEnumProvider[]
	{
		return [
		    { id: 1, name: 'Admin', description: 'Administrator', bgColor: '' },
		    { id: 2, name: 'Member', description: 'Member', bgColor: '' },
		    { id: 3, name: 'UserAdmin', description: 'User Admin', bgColor: '' },
		    { id: 4, name: 'Moderator', description: 'Moderator', bgColor: '' }
		];
	}
}
