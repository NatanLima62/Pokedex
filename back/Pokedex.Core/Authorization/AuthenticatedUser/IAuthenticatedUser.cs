namespace Pokedex.Core.Authorization.AuthenticatedUser;

public interface IAuthenticatedUser
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public bool IsLoggedUser { get; }
}