using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlocacaoVeiculosAssistencia.Infrastructure.Data.Configurations;

public class PlanosAssistenciaConfiguration
    : IEntityTypeConfiguration<PlanosAssistencia>
{
    public void Configure(EntityTypeBuilder<PlanosAssistencia> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Plano)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ValorCobertura)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.PlanosAssistencia)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}