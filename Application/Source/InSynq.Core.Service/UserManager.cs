using InSynq.Core.Model.Models.Application.User;
using InSynq.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Nexus.Core.Service;

public class UserManager(IDatabaseContext db, ICloudinaryService cloudinaryService) : IUserManager
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string password, string storedPassword) => BCrypt.Net.BCrypt.Verify(password, storedPassword);

    public async Task LockUserAsync(string email)
    {
        var model = await db.Users.GetSingleAsync(_ => _.Email == email);
        model.IsLocked = true;
        await db.SaveChangesAsync();
    }

    public async Task UnlockUserAsync(string email)
    {
        var model = await db.Users.GetSingleAsync(_ => _.Email == email);
        if (model.IsLocked == true)
        {
            model.IsLocked = false;
            await db.SaveChangesAsync();
        }
    }

    public async Task<bool> IsUserLockedAsync(string email) => (await db.Users.Where(_ => _.Email == email).Select(_ => _.IsLocked).FirstOrDefaultAsync()) == true;

    public async Task<ResponseWrapper> UploadPhotoAsync(User user, IFormFile photo)
    {
        if (photo.IsNullOrEmpty())
            return new();

        var result = await cloudinaryService.UploadPhotoAsync(photo);

        if (result.Error != null)
            return new(new Error("Image", "There was an error while uploading photo."));

        user.ProfileImageUrl = result.SecureUrl.AbsoluteUri;
        user.PublicId = result.PublicId;

        return new();
    }

    public async Task<ResponseWrapper> DeletePhotoAsync(User user)
    {
        if (user.PublicId.IsNullOrWhiteSpace())
            return new(new Error("Image", Constants.ERROR_INVALID_OPERATION));

        var result = await cloudinaryService.DeletePhotoAsync(user.PublicId);

        if (result.Error != null)
            return new(new Error("Image", "There was an error while deleting photo."));

        user.ProfileImageUrl = null;
        user.PublicId = null;
        return new();
    }
}