using Autofac;
using InSynq.Infrastructure.DependencyRegister.Mappings;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class MainModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterModule<InfrastructureModule>();
		builder.RegisterModule<MapperModule>();
		builder.RegisterModule<ServiceModule>();
	}
}