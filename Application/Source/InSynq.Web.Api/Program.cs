using Autofac;
using Autofac.Extensions.DependencyInjection;
using InSynq.Core.Model.Interfaces;
using InSynq.Infrastructure.Data;
using InSynq.Infrastructure.DependencyRegister.Modules;
using InSynq.Web.Api.Middlewares;
using InSynq.Web.Api.Objects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DatabaseContext>();

builder.Host
	.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(container =>
	{
		container.RegisterType<IdentityUser>().As<IIdentityUser>().InstancePerLifetimeScope().PropertiesAutowired();
		container.RegisterModule<MainModule>();
	});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
	var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

	try
	{
		var migrationResult = context.Migrate();

		logger.LogInformation("{result}", migrationResult.Result);
		migrationResult.Migrations.ForEach(_ => logger.LogInformation("{log}", _));
	}
	catch (Exception ex)
	{
		logger.LogError("MIGRATION - FAILED");
		logger.LogError(ex, "An error occurred while migrating the database.");
	}
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();

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