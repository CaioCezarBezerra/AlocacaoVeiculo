using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Data.Repository
{
    public class PlanoAssistenciaRepository : IPlanoAssistenciaRepository
    {
        private readonly AppDbContext _context;

        public PlanoAssistenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanosAssistencia>>
            ListarPlanosAssistenciaAsync()
        {
            return await _context.PlanosAssistencia
                .AsNoTracking()
                .Include(x => x.Empresa)
                .ToListAsync();
        }

        public async Task<PlanosAssistencia?>
            ObterPlanoAssistenciaPorIdAsync(int id)
        {
            return await _context.PlanosAssistencia
                .AsNoTracking()
                .Include(x => x.Empresa)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PlanosAssistencia>
            CriarPlanoAssistenciaAsync(PlanosAssistencia plano)
        {
            await _context.PlanosAssistencia.AddAsync(plano);

            await _context.SaveChangesAsync();

            return plano;
        }

        public async Task<PlanosAssistencia?>
            AtualizarPlanoAssistenciaAsync(
                int id,
                PlanosAssistencia plano)
        {
            var planoExiste = await _context.PlanosAssistencia
                .FirstOrDefaultAsync(x => x.Id == id);

            if (planoExiste == null)
                return null;

            planoExiste.Plano = plano.Plano;
            planoExiste.Descricao = plano.Descricao;
            planoExiste.ValorCobertura = plano.ValorCobertura;
            planoExiste.EmpresaId = plano.EmpresaId;

            await _context.SaveChangesAsync();

            return planoExiste;
        }

        public async Task<bool>
            DeletarPlanoAssistenciaAsync(int id)
        {
            var plano = await _context.PlanosAssistencia
                .FirstOrDefaultAsync(x => x.Id == id);

            if (plano == null)
                return false;

            _context.PlanosAssistencia.Remove(plano);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}