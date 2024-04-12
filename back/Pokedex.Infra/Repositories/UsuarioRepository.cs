using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Contracts;

namespace Pokedex.Infra.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    protected UsuarioRepository(PokedexDbContext context) : base(context)
    {
    }

    public void Adicionar(Usuario usuario)
    {
        Context.Usuarios.Add(usuario);
    }

    public void Atualizar(Usuario usuario)
    {
        Context.Usuarios.Update(usuario);
    }

    public async Task<List<Usuario>> ObterTodos()
    {
        return await Context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        return await Context.Usuarios.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        return await Context.Usuarios.FirstOrDefaultAsync(c => c.Email == email);
    }

    public void Remover(Usuario usuario)
    {
        Context.Usuarios.Remove(usuario);
    }
}