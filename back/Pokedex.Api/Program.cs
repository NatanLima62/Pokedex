using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Pokedex.Api.Configurations;
using Pokedex.Application;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .Configure<RequestLocalizationOptions>(o =>
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        o.DefaultRequestCulture = new RequestCulture("pt-BR", "pt-BR");
        o.SupportedCultures = supportedCultures;
        o.SupportedUICultures = supportedCultures;
    });

builder
    .Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.SetupSettings(builder.Configuration);
builder.Services.ConfigureDependencies(builder.Configuration);
builder.Services.ResolveApplicationDependencies();

var app = builder.Build();

app.UseSwaggerConfig();

app.UseHttpsRedirection();

app.UseAuthenticationConfig();

app.MapControllers();

app.Run();