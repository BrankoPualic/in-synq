using Autofac;
using AutoMapper;

namespace InSynq.Infrastructure.DependencyRegister.Mappings;

public class MapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
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