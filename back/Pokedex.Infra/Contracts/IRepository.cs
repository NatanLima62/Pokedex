using System.Linq.Expressions;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Contracts;

public interface IRepository<T> : IDisposable where T : BaseEntity, IAggregateRoot
{
    public IUnitOfWork UnitOfWork { get; }
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    public Task<bool> Any(Expression<Func<T, bool>> predicate); 
}