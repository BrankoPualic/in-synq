using Autofac;
using AutoMapper;
using InSynq.Core.Model;
using InSynq.Infrastructure.Data;
using InSynq.Infrastructure.DependencyRegister.Mappings;

namespace InSynq.Infrastructure.DependencyRegister.Modules;

public class InfrastructureModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<DatabaseContext>().As<IDatabaseContext>();

		builder.Register(ctx =>
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<ApplicationProfile>();
			});

			var mapper = config.CreateMapper();
			return mapper;
		}).As<IMapper>().SingleInstance();
	}
}