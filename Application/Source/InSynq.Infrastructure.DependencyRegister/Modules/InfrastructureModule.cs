using Autofac;
using InSynq.Core.Model;
using InSynq.Infrastructure.Data;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class InfrastructureModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<DatabaseContext>().As<IDatabaseContext>();
	}
}