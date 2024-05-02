using Pokedex.Domain.Entities;

namespace Pokedex.Application.Contracts;

public interface IEmailService
{
    void SendEmailRecoverPassword(User user);
}