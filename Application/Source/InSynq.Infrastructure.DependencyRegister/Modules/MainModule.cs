using Autofac;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class MainModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<InfrastructureModule>();
		builder.RegisterModule<ServiceModule>();
	}
}