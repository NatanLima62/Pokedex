using FluentValidation.Results;

namespace Pokedex.Application.Notifications;

public interface INotificator
{
    public void Handle(string error);
    public void Handle(List<ValidationFailure> errors);
    public void HandleNotFoundResource();
    public bool HasNotifications();
    public IEnumerable<string> GetNotifications();
    public bool IsNotFoundResource { get; }
}