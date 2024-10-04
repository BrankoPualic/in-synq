using InSynq.Core.Interfaces.Person;

namespace InSynq.Test;

[TestFixture]
public partial class SandboxUT : BaseUT
{
    [Test, Explicit]
    public async Task SandboxBPR()
    {
        var servicelock = Get<IUserService>();
        var result = await servicelock.GetSingleAsync(1);
    }
}