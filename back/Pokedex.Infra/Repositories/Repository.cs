using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Contracts;

namespace Pokedex.Infra.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly DbSet<T> _dbSet;
    protected readonly PokedexDbContext Context;

    protected Repository(PokedexDbContext context)
    {
        Context = context;
        _dbSet = Context.Set<T>();
    }
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }
    
    public IUnitOfWork UnitOfWork => Context;
    public void Dispose() => Context.Dispose();
}