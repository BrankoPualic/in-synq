using Autofac;
using InSynq.Core.Model.Interfaces;
using InSynq.Core.Model.Models;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class UnitTestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<MainModule>();

        builder.RegisterType<MockIdentityUser>().As<IIdentityUser>().InstancePerLifetimeScope();

        base.Load(builder);
    }
}