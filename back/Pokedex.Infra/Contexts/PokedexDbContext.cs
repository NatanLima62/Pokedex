using System.Reflection;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Authorization.AuthenticatedUser;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Extensions;

namespace Pokedex.Infra.Contexts;

public class PokedexDbContext : DbContext, IUnitOfWork
{
    private readonly IAuthenticatedUser _authenticatedUser;
    public PokedexDbContext(DbContextOptions<PokedexDbContext> options, IAuthenticatedUser authenticatedUser) : base(options)
    {
        _authenticatedUser = authenticatedUser;
    }

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
            ((ITracking)entityEntry.Entity).UpdatedBy = _authenticatedUser.Id;
            if (entityEntry.State != EntityState.Added)
                continue;

            ((ITracking)entityEntry.Entity).CreatedAt = DateTime.Now;
            ((ITracking)entityEntry.Entity).CreatedBy = _authenticatedUser.Id;
        }
    }
}