using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pokedex.Domain.Contracts;

namespace Pokedex.Infra.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyEntityConfiguration(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.GetEntities<IEntity>();
        var props = entities.SelectMany(c => c.GetProperties()).ToList();
        foreach (var property in props.Where(c => c.ClrType == typeof(int) && c.Name == "Id"))
        {
            property.IsKey();
        }
    }

    public static void ApplyTrackingConfiguration(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.GetEntities<ITracking>();
        var dataProps = entityTypes.SelectMany(c => c.GetProperties().Where(p => p.ClrType == typeof(DateTime) && new[] { "CreatedAt", "UpdatedAt" }.Contains(p.Name)));
        foreach (var prop in dataProps)
        {
            prop.SetColumnType("timestamp");
            prop.SetDefaultValueSql("CURRENT_TIMESTAMP");
        }
        
        var idsProps = entityTypes.SelectMany(c => c.GetProperties().Where(p => p.ClrType == typeof(int) && new[] {"CreatedBy", "UpdatedBy"}.Contains(p.Name)));
        foreach (var prop in idsProps)
        {
            prop.IsNullable = true;
        }
    }
    
    public static void ApplySoftDeleteConfiguration(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.GetEntities<ISoftDelete>();
        var props = entityTypes.SelectMany(c => c.GetProperties().Where(p => p.ClrType == typeof(bool))).ToList();
        foreach (var prop in props.Where(c => c.Name == "Disabled"))
        {
            prop.IsNullable = false;
            prop.SetDefaultValue(false);
        }
    }
    
    public static void ApplyStringConfiguration(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();
        var props = entityTypes.SelectMany(c => c.GetProperties().Where(p => p.ClrType == typeof(string))).ToList();
        foreach (var prop in props)
        {
            prop.SetMaxLength(255);
        }
    }

    private static List<IMutableEntityType> GetEntities<T>(this ModelBuilder modelBuilder)
    {
        return modelBuilder.Model.GetEntityTypes().Where(c => c.ClrType.GetInterface(nameof(T)) != null).ToList();
    }
}