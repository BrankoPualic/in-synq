using Autofac;
using InSynq.Core;
using InSynq.Core.Interfaces;
using InSynq.Core.Service.Services;
using Nexus.Core.Service;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class ServiceModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		// User Management
		builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
		builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
		// Provider Service
		builder.RegisterType<ProviderService>().As<IProviderService>().InstancePerLifetimeScope();
		// Auth
		builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

		base.Load(builder);
	}
}