using Autofac;
using InSynq.Core;
using Nexus.Core.Service;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
		builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
	}
}