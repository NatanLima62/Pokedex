using Microsoft.AspNetCore.Http;

namespace Pokedex.Core.Extensions;

public static class HttpContextAccessorExtensions
{
    public static bool AuthenticatedUser(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext?.User.AuthenticatedUser() ?? false;
    }

    public static int? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var id = httpContextAccessor.HttpContext?.User.GetUserId();
        return string.IsNullOrWhiteSpace(id) ? null : int.Parse(id);
    }

    public static string GetUserName(this IHttpContextAccessor httpContextAccessor)
    {
        var name = httpContextAccessor.HttpContext?.User.GetUserName();
        return string.IsNullOrWhiteSpace(name) ? string.Empty : name;
    }

    public static string GetUserEmail(this IHttpContextAccessor httpContextAccessor)
    {
        var email = httpContextAccessor.HttpContext?.User.GetUserEmail();
        return string.IsNullOrWhiteSpace(email) ? string.Empty : email;
    }
}