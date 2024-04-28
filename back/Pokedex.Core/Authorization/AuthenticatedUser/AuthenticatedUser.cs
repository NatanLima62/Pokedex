using Microsoft.AspNetCore.Http;
using Pokedex.Core.Extensions;

namespace Pokedex.Core.Authorization.AuthenticatedUser;

public class AuthenticatedUser : IAuthenticatedUser
{
    public AuthenticatedUser()
    {
    }

    public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
    {
        Id = httpContextAccessor.GetUserId()!.Value;
        Name = httpContextAccessor.GetUserName();
        Email = httpContextAccessor.GetUserEmail();
    }

    public int Id { get; } = -1;
    public string Name { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public bool IsLoggedUser => Id > 0;
}