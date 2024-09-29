using InSynq.Core.Dtos.Auth;
using InSynq.Core.Model.Models.Application.User;

namespace InSynq.Core.Service.Services;

public class AuthService(IDatabaseContext context, ITokenService tokenService, IUserManager userManager, ILockoutService lockoutService) : BaseService(context), IAuthService
{
    public async Task<ResponseWrapper<TokenDto>> Signin(SigninDto data)
    {
        if (!data.IsValid())
            return new(data.Errors);

        var model = await db.Users.GetSingleAsync(_ => _.Email == data.Email, _ => _.Roles);
        if (model.IsNullOrEmpty())
            return new(new Error(nameof(User), ResourceValidation.Invalid_Credentials));

        // Check if the user is locked
        var result = await lockoutService.IsUserLockedAsync(data.Email);
        if (!result.IsSuccess)
            return new(result.Errors);

        // Register failed attempt if passwords do not match
        if (!userManager.VerifyPassword(data.Password, model.Password))
        {
            await lockoutService.RegisterFailedAttemptAsync(data.Email);
            return new(new Error(nameof(User), ResourceValidation.Invalid_Credentials));
        }

        // Successful signin
        await lockoutService.ResetFailedAttemptsAsync(data.Email);

        // Signin log
        var signin = new UserSigninLog(model.Id);
        db.Create(signin);
        await db.SaveChangesAsync(false);

        var token = new TokenDto { Token = tokenService.GenerateJwtToken(model) };

        return new(token);
    }

    public async Task<ResponseWrapper<TokenDto>> Signup(SignupDto data)
    {
        if (!data.IsValid())
            return new(data.Errors);

        var existingEmail = await db.Users.GetSingleAsync(_ => _.Email.Equals(data.Email));
        if (existingEmail.IsNotNullOrEmpty())
            return new(new Error(nameof(User.Email), ResourceValidation.Already_Exist.FormatWith(nameof(User), nameof(User.Email))));

        var existingUsername = await db.Users.GetSingleAsync(_ => _.Username.Equals(data.Username));
        if (existingUsername.IsNotNullOrEmpty())
            return new(new Error(nameof(User.Username), ResourceValidation.Already_Exist.FormatWith(nameof(User), nameof(User.Username))));

        User model = new();
        data.ToModel(model);
        model.Password = userManager.HashPassword(data.Password);

        // Upload photo
        var uploadResult = await userManager.UploadPhotoAsync(model, data.Photo);
        if (!uploadResult.IsSuccess)
        {
            var uploadErrors = uploadResult.Errors.GetValue("Image");
            uploadErrors.ForEach(_ => data.Errors.AddError("Image", _));
            return new(data.Errors);
        }

        db.Create(model);
        await db.SaveChangesAsync();

        var token = new TokenDto { Token = tokenService.GenerateJwtToken(model) };

        return new(token);
    }
}