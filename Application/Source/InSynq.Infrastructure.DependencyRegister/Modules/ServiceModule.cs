using Autofac;
using InSynq.Core;
using InSynq.Core.Interfaces;
using InSynq.Core.Interfaces.Follow;
using InSynq.Core.Interfaces.Person;
using InSynq.Core.Service.Services;
using InSynq.Core.Service.Services.Follow;
using InSynq.Core.Service.Services.Person;
using StackExchange.Redis;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class ServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // User Management
        builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
        builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
        // Redis
        builder.Register(c =>
        {
            var configuration = ConfigurationOptions.Parse("localhost:6379");
            configuration.AllowAdmin = true;
            return ConnectionMultiplexer.Connect(configuration);
        }).As<IConnectionMultiplexer>().SingleInstance();
        builder.RegisterType<LockoutService>().As<ILockoutService>().SingleInstance();
        // Provider Service
        builder.RegisterType<ProviderService>().As<IProviderService>().InstancePerLifetimeScope();
        // Auth
        builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();

        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<FollowService>().As<IFollowService>().InstancePerLifetimeScope();
        builder.RegisterType<DocumentService>().As<IDocumentService>().InstancePerLifetimeScope();

        base.Load(builder);
    }
}