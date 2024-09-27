﻿using InSynq.Core.Model.Models.Application.User;
using Microsoft.AspNetCore.Http;

namespace InSynq.Core;

public interface IUserManager
{
	string HashPassword(string password);

	bool VerifyPassword(string password, string storedPassword);

	Task LockUserAsync(string email);

	Task UnlockUserAsync(string email);

	Task<bool> IsUserLockedAsync(string email);

	Task<ResponseWrapper> UploadPhotoAsync(User user, IFormFile photo);

	Task<ResponseWrapper> DeletePhotoAsync(User user);
}