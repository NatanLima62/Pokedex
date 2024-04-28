namespace Pokedex.Domain.Contracts;

public interface ISoftDelete
{
    public bool Disabled { get; set; }
}