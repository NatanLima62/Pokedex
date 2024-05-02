namespace Pokedex.Application.Dtos.Auth;

public class ChangePasswordUserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public Guid? Token { get; set; }
}