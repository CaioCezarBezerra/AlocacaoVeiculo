using AlocacaoVeiculosAssistencia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<GruposVeiculos> GruposVeiculos => Set<GruposVeiculos>();

    public DbSet<Veiculos> Veiculos => Set<Veiculos>();

    public DbSet<EmpresasAssistencia> EmpresasAssistencia
        => Set<EmpresasAssistencia>();

    public DbSet<PlanosAssistencia> PlanosAssistencia
        => Set<PlanosAssistencia>();

    public DbSet<VeiculosAssistencia> VeiculosAssistencia
        => Set<VeiculosAssistencia>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }
}