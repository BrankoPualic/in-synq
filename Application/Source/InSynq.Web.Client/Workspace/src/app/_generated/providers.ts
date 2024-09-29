import { Injectable } from '@angular/core';
import { IEnumProvider } from './interfaces';

@Injectable() export class Providers
{
	getGenders() : IEnumProvider[]
	{
		return [
		    { id: 0, name: 'NotSet', description: '' },
		    { id: 1, name: 'Male', description: 'Male' },
		    { id: 2, name: 'Female', description: 'Female' },
		    { id: 3, name: 'Other', description: 'Other' }
		];
	}
	getSystemRoles() : IEnumProvider[]
	{
		return [
		    { id: 1, name: 'Admin', description: 'Administrator' },
		    { id: 2, name: 'Member', description: 'Member' },
		    { id: 3, name: 'UserAdmin', description: 'User Admin' },
		    { id: 4, name: 'Moderator', description: 'Moderator' }
		];
	}
}
