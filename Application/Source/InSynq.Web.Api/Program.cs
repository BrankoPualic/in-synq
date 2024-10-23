using Autofac;
using Autofac.Extensions.DependencyInjection;
using InSynq.Core.Model.Interfaces;
using InSynq.Infrastructure.Data;
using InSynq.Infrastructure.DependencyRegister.Modules;
using InSynq.Web.Api.Middlewares;
using InSynq.Web.Api.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = InSynq.Common.Settings.Issuer,
        ValidAudience = InSynq.Common.Settings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(InSynq.Common.Settings.JwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

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