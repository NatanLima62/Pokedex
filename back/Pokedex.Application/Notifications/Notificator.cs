using FluentValidation.Results;

namespace Pokedex.Application.Notifications;

public class Notificator : INotificator
{
    private readonly List<string> _erros;
    public IEnumerable<string> GetNotifications()
    {
        return _erros;
    }

    public bool IsNotFoundResource { get; private set; }
    
    public Notificator()
    {
        _erros = new List<string>();
    }

    public void Handle(string error)
    {
        if (IsNotFoundResource)
        {
            throw new ArgumentException("Não é possível chamar um Handle quando for um tipo NotFoundResource");
        }
        
        _erros.Add(error);
    }

    public void Handle(List<ValidationFailure> errors)
    {
        errors.ForEach(error => Handle(error.ErrorMessage));
    }


    public void HandleNotFoundResource()
    {
        if (!IsNotFoundResource)
        {
            throw new ArgumentException("Não é possível chamar um NotFoundResource quando for um tipo Handle");
        }
        
        IsNotFoundResource = true;
    }

    public bool HasNotifications() => _erros.Any();
}