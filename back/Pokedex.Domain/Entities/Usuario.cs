using FluentValidation.Results;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Validators;

namespace Pokedex.Domain.Entities;

public class Usuario : Entity, ISoftDelete, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public string? Foto { get; set; }
    public bool Desativado { get; set; }

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new UsuarioValidator().Validate(this);
        return validationResult.IsValid;
    }
}