namespace Pokedex.Domain.Contracts;

public interface ITracking
{
    public int CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public int ModificadoPor { get; set; }
    public DateTime ModificadoEm { get; set; }
}