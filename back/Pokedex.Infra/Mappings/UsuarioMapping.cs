using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder
            .Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Senha)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(c => c.Foto)
            .IsRequired(false)
            .HasMaxLength(255);
    }
}