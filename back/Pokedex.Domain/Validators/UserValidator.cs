using FluentValidation;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .MaximumLength(255)
            .WithMessage("O nome deve ter no máximo 255 caracteres");
        
        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("O email é inválido");

        RuleFor(u => u.Password)
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres");
    }
}