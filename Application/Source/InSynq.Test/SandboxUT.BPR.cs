using InSynq.Core.Interfaces;

namespace InSynq.Test;

[TestFixture]
public partial class SandboxUT : BaseUT
{
	[Test, Explicit]
	public async Task SandboxBPR()
	{
		var service = Get<IProviderService>();
		var result = await service.GetCountriesAsync();
	}
}