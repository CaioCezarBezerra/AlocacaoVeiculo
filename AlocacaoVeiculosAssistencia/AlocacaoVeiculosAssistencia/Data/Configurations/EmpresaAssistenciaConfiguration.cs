using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlocacaoVeiculosAssistencia.Infrastructure.Data.Configurations;

public class EmpresaAssistenciaConfiguration
    : IEntityTypeConfiguration<EmpresasAssistencia>
{
    public void Configure(EntityTypeBuilder<EmpresasAssistencia> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Endereco)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(x => x.PlanosAssistencia)
            .WithOne(x => x.Empresa)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}