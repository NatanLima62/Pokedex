namespace Pokedex.Domain.Contracts;

public interface IUnitOfWork
{
    public Task<bool> Commit();
}