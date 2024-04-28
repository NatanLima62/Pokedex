using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Contracts;

public interface IUserRepository : IRepository<User>
{
    void Add(User user);
    void Update(User user);
    Task<List<User>> GetAll();
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    void Remove(User user);
}