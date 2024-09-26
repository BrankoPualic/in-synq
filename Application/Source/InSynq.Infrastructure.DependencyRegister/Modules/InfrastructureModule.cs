using Autofac;
using InSynq.Core.Model;
using InSynq.Infrastructure.Data;
using InSynq.Infrastructure.Interfaces;
using InSynq.Infrastructure.Storage;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class InfrastructureModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<DatabaseContext>().As<IDatabaseContext>().InstancePerLifetimeScope();
		builder.RegisterType<CloudinaryService>().As<ICloudinaryService>().InstancePerLifetimeScope();

		base.Load(builder);
	}
}