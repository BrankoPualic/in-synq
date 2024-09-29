using InSynq.Core.Interfaces;

namespace InSynq.Test;

[TestFixture]
public partial class SandboxUT : BaseUT
{
    [Test, Explicit]
    public async Task SandboxBPR()
    {
        //var service = Get<IAuthService>();

        //var user = new SigninDto
        //{
        //    Email = "branko@insinq.com",
        //    Password = "Pa$$w0rd"
        //};

        var servicelock = Get<ILockoutService>();
        await servicelock.ResetFailedAttemptsAsync("branko@insinq.com");

        //var result = await service.Signin(user);
    }
}