using FluentValidation.Results;
using Pokedex.Domain.Contracts;

namespace Pokedex.Domain.Entities;

public abstract class Entity : BaseEntity, ITracking 
{
    public int CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; }
    public int ModificadoPor { get; set; }
    public DateTime ModificadoEm { get; set; }

    public virtual bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    } 
}

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}