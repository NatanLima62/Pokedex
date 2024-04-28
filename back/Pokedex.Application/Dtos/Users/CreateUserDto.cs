using Microsoft.AspNetCore.Http;

namespace Pokedex.Application.Dtos.Users;

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public IFormFile? Image { get; set; }
}