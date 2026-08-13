using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VeiculoAssistenciaConfiguration
    : IEntityTypeConfiguration<VeiculosAssistencia>
{
    public void Configure(EntityTypeBuilder<VeiculosAssistencia> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Veiculo)
            .WithMany(x => x.Assistencias)
            .HasForeignKey(x => x.VeiculoId);

        builder.HasOne(x => x.Plano)
            .WithMany(x => x.Veiculos)
            .HasForeignKey(x => x.PlanoId);

        builder.HasIndex(x => new
        {
            x.VeiculoId,
            x.PlanoId
        })
        .IsUnique();
    }
}