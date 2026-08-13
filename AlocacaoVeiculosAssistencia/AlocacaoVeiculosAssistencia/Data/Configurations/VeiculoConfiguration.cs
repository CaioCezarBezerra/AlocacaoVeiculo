using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlocacaoVeiculosAssistencia.Infrastructure.Data.Configurations;

public class VeiculosConfiguration
    : IEntityTypeConfiguration<Veiculos>
{
    public void Configure(EntityTypeBuilder<Veiculos> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Modelo)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Placa)
            .IsRequired()
            .HasMaxLength(7);

        
        builder.HasIndex(x => x.Placa)
            .IsUnique();

        
        builder.HasOne(x => x.Grupo)
            .WithMany(x => x.Veiculos)
            .HasForeignKey(x => x.GrupoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Assistencias)
            .WithOne(x => x.Veiculo)
            .HasForeignKey(x => x.VeiculoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}