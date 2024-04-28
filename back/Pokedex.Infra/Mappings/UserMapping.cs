using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(c => c.Picture)
            .IsRequired(false)
            .HasMaxLength(255);
    }
}