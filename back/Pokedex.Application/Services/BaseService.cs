using AutoMapper;
using Pokedex.Application.Notifications;

namespace Pokedex.Application.Services;

public abstract class BaseService
{
    protected readonly IMapper Mapper;
    protected readonly INotificator Notificator;

    protected BaseService(IMapper mapper, INotificator notificator)
    {
        Mapper = mapper;
        Notificator = notificator;
    }
}