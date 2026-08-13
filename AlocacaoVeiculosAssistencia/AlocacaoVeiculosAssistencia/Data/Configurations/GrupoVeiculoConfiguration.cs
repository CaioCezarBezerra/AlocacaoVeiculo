using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlocacaoVeiculosAssistencia.Infrastructure.Data.Configurations;

public class GrupoVeiculosConfiguration
    : IEntityTypeConfiguration<GruposVeiculos>
{
    public void Configure(EntityTypeBuilder<GruposVeiculos> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(x => x.Veiculos)
            .WithOne(x => x.Grupo)
            .HasForeignKey(x => x.GrupoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}