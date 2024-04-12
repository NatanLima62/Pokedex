using FluentValidation;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Validators;

public class UsuarioValidator : AbstractValidator<Usuario>
{
    public UsuarioValidator()
    {
        RuleFor(u => u.Nome)
            .MaximumLength(255)
            .WithMessage("O nome deve ter no máximo 255 caracteres");
        
        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("O email é inválido");

        RuleFor(u => u.Senha)
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres");
    }
}