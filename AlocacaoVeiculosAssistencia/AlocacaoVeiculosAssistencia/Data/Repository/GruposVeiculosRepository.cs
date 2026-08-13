using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Data.Repository
{
    public class GruposVeiculosRepository : IGrupoVeiculosRepository
    {
        private readonly AppDbContext _context;

        public GruposVeiculosRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<GruposVeiculos> AtualizarGrupoVeiculoAsync(int id, GruposVeiculos grupoVeiculos)
        {
            var grupoExiste = await _context.GruposVeiculos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (grupoExiste == null)
                return null;

            grupoExiste.Nome = grupoVeiculos.Nome;
            grupoExiste.Descricao = grupoVeiculos.Descricao;


            await _context.SaveChangesAsync();

            return grupoExiste;
        }

        public async Task<GruposVeiculos> CriarGrupoVeiculoAsync(GruposVeiculos grupoVeiculos)
        {
            _context.GruposVeiculos.Add(grupoVeiculos);
            await _context.SaveChangesAsync();
            return grupoVeiculos;
        }
        

        public async Task<bool> DeletarGrupoVeiculoAsync(int id)
        {
            var grupoExiste = await _context.GruposVeiculos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (grupoExiste == null)
                return false;

            _context.GruposVeiculos.Remove(grupoExiste);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GruposVeiculos>> ListarGrupoVeiculosAsync()
        {
            return await _context.GruposVeiculos.ToListAsync();
        }
        

        public async Task<GruposVeiculos> ObterGrupoVeiculoPorIdAsync(int id)
        {
            return await _context.GruposVeiculos.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
