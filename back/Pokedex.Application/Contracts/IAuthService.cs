using Pokedex.Application.Dtos.Auth;
using Pokedex.Application.Dtos.Users;

namespace Pokedex.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto dto);
}