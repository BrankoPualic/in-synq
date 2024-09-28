using InSynq.Core.Dtos.Auth;
using InSynq.Core.Interfaces;

namespace InSynq.Test;

[TestFixture]
public partial class SandboxUT : BaseUT
{
    [Test, Explicit]
    public async Task SandboxBPR()
    {
        var service = Get<IAuthService>();

        var user = new SigninDto
        {
            Email = "sysmember@insinq.com",
            Password = "Pa$$w0rd"
        };

        var servicelock = Get<ILockoutService>();
        await servicelock.ResetFailedAttemptsAsync("sysmember@insinq.com");

        var result = await service.Signin(user);
    }
}