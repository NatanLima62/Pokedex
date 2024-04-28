using FluentValidation.Results;
using Pokedex.Domain.Contracts;

namespace Pokedex.Domain.Entities;

public abstract class Entity : BaseEntity, ITracking 
{
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }

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