using System.Reflection;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Extensions;

namespace Pokedex.Infra.Contexts;

public class PokedexDbContext : DbContext, IUnitOfWork
{
    public PokedexDbContext(DbContextOptions<PokedexDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = null!;
    
    public async Task<bool> Commit() => await SaveChangesAsync() > 0;
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .HasCharSet("utf8mb4")
            .UseCollation("utf8mb4_unicode_ci")
            .UseGuidCollation(string.Empty);
        
        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
        modelBuilder.ApplySoftDeleteConfiguration();
        modelBuilder.ApplyStringConfiguration();
        
        base.OnModelCreating(modelBuilder);
    }
    
    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker.Entries().Where(e =>
            e.Entity is ITracking && e.State is EntityState.Modified || e.State is EntityState.Added);

        foreach (var entityEntry in entries)
        {
            //Todo: Adicionar a logica para UpdatedBy e CreatedBy do usuario logado
            ((ITracking)entityEntry.Entity).UpdatedAt = DateTime.Now;
            if (entityEntry.State != EntityState.Added)
                continue;

            ((ITracking)entityEntry.Entity).CreatedAt = DateTime.Now;
        }
    }
}