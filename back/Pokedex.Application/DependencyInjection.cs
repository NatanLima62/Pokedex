using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt.Core.Interfaces;
using Pokedex.Application.Contracts;
using Pokedex.Application.Notifications;
using Pokedex.Application.Services;
using Pokedex.Core.Settings;
using Pokedex.Domain.Entities;
using Pokedex.Infra;
using ScottBrady91.AspNetCore.Identity;

namespace Pokedex.Application;

public static class DependencyInjection
{
    public static void SetupSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    } 
    
    public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigDbContext(configuration);
        services.ResolveInfraDependencies();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
    
    public static void ResolveApplicationDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>()

            .AddScoped<INotificator, Notificator>();
        
        services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserService, UserService>();
    }
}