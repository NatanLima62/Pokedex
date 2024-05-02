using FluentValidation.Results;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Validators;

namespace Pokedex.Domain.Entities;

public class User : Entity, ISoftDelete, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Picture { get; set; }
    public bool Disabled { get; set; }
    public Guid? TokenRecoverPassword { get; set; }
    public DateTime? TokenExpiresIn { get; set; }

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new UserValidator().Validate(this);
        return validationResult.IsValid;
    }
}