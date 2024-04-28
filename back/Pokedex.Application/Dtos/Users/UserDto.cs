namespace Pokedex.Application.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Picture { get; set; }
    public bool Disabled { get; set; }
}