using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Data.Repository
{
    public class VeiculoAssistenciaRepository : IVeiculoAssistenciasRepository
    {
        private readonly AppDbContext _context;

        public VeiculoAssistenciaRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<VeiculosAssistencia>>
            ListarVeiculoAssistenciasAsync()
        {
            return await _context.VeiculosAssistencia
                .AsNoTracking()
                .Include(x => x.Veiculo)
                .Include(x => x.Plano)
                .ToListAsync();
        }


        public async Task<VeiculosAssistencia?>
            ObterVeiculoAssistenciaPorIdAsync(int id)
        {
            return await _context.VeiculosAssistencia
                .AsNoTracking()
                .Include(x => x.Veiculo)
                .Include(x => x.Plano)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<VeiculosAssistencia>
            CriarVeiculoAssistenciaAsync(
                VeiculosAssistencia veiculoAssistencia)
        {
            await _context.VeiculosAssistencia
                .AddAsync(veiculoAssistencia);

            await _context.SaveChangesAsync();

            return veiculoAssistencia;
        }


        public async Task<VeiculosAssistencia?>
            AtualizarVeiculoAssistenciaAsync(
                int id,
                VeiculosAssistencia veiculoAssistencia)
        {
            var vinculoExistente =
                await _context.VeiculosAssistencia
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (vinculoExistente == null)
                return null;

            vinculoExistente.VeiculoId =
                veiculoAssistencia.VeiculoId;

            vinculoExistente.PlanoId =
                veiculoAssistencia.PlanoId;

            await _context.SaveChangesAsync();

            return vinculoExistente;
        }


        public async Task<bool> DeletarVeiculoAssistenciaAsync(int id)
        {
            var vinculoExistente =
                await _context.VeiculosAssistencia
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (vinculoExistente == null)
                return false;

            _context.VeiculosAssistencia
                .Remove(vinculoExistente);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExisteVinculoAsync(
    int veiculoId,
    int planoId)
        {
            return await _context.VeiculosAssistencia
                .AnyAsync(x =>
                    x.VeiculoId == veiculoId &&
                    x.PlanoId == planoId);
        }
    }
}