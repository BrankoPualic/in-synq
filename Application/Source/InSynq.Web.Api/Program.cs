using Autofac;
using Autofac.Extensions.DependencyInjection;
using InSynq.Core.Model.Interfaces;
using InSynq.Infrastructure.DependencyRegister.Modules;
using InSynq.Web.Api.Objects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Host
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(container =>
	{
		container.RegisterType<IdentityUser>().As<IIdentityUser>().InstancePerLifetimeScope().PropertiesAutowired();
		container.RegisterModule<MainModule>();
	});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder => builder
	.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowCredentials()
	.WithOrigins(InSynq.Common.Settings.Audience)
	);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();