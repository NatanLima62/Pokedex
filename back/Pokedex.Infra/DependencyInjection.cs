using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Core.Authorization.AuthenticatedUser;
using Pokedex.Core.Extensions;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Contracts;
using Pokedex.Infra.Repositories;

namespace Pokedex.Infra;

public static class DependencyInjection
{
    public static void ConfigDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthenticatedUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return httpContextAccessor.AuthenticatedUser()
                ? new AuthenticatedUser(httpContextAccessor)
                : new AuthenticatedUser();
        });
        
        services.AddDbContext<PokedexDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
    
    public static void ResolveInfraDependencies(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>();
    }
}