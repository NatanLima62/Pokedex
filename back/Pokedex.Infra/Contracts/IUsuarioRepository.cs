using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Contracts;

public interface IUsuarioRepository : IRepository<Usuario>
{
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    Task<List<Usuario>> ObterTodos();
    Task<Usuario?> ObterPorId(int id);
    Task<Usuario?> ObterPorEmail(string email);
    void Remover(Usuario usuario);
}