using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Contracts;

namespace Pokedex.Infra.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(PokedexDbContext context) : base(context)
    {
    }

    public void Add(User user)
    {
        Context.Users.Add(user);
    }

    public void Update(User user)
    {
        Context.Users.Update(user);
    }

    public async Task<List<User>> GetAll()
    {
        return await Context.Users.ToListAsync();
    }

    public async Task<User?> GetById(int id)
    {
        return await Context.Users.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await Context.Users.FirstOrDefaultAsync(c => c.Email == email);
    }

    public void Remove(User user)
    {
        Context.Users.Remove(user);
    }
}