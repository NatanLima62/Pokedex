using Microsoft.AspNetCore.Http;

namespace Pokedex.Application.Dtos.Users;

public class UpdateUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IFormFile? Image { get; set; }
}