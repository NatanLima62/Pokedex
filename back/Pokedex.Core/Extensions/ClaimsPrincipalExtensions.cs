using System.Security.Claims;

namespace Pokedex.Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool AuthenticatedUser(this ClaimsPrincipal? principal)
    {
        return principal?.Identity?.IsAuthenticated ?? false;
    }
    
    public static string? GetUserId(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.NameIdentifier);
    
    public static string? GetUserName(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.Name);
    
    public static string? GetUserEmail(this ClaimsPrincipal? principal) => GetClaim(principal, ClaimTypes.Email);
    
    private static string? GetClaim(ClaimsPrincipal? principal, string claimName)
    {
        if (principal == null)
        {
            throw new ArgumentException(null, nameof(principal));
        }

        var claim = principal.FindFirst(claimName);
        return claim?.Value;
    }
}