using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Contexts;

public class PokedexDbContext : DbContext, IUnitOfWork
{
    public PokedexDbContext(DbContextOptions<PokedexDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; } = null!;
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
}