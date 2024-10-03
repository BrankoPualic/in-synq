using InSynq.Core.Interfaces.Person;
using InSynq.Core.Search;

namespace InSynq.Test;

[TestFixture]
public partial class SandboxUT : BaseUT
{
    [Test, Explicit]
    public async Task SandboxBPR()
    {
        var options = new UserSearchOptions
        {
            Name = "admin"
        };
        var servicelock = Get<IUserService>();
        var result = await servicelock.SearchAsync(options);
    }
}