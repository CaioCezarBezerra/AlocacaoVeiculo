using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Data.Repository
{
    public class EmpresaAssistenciaRepository : IEmpresasAssistenciaRepository
    {

        private readonly AppDbContext _context;

        public EmpresaAssistenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmpresasAssistencia?> AtualizarEmpresaAssistenciaAsync(int id, EmpresasAssistencia empresa)
        {
            var empresaExiste = await _context.EmpresasAssistencia
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empresaExiste == null)
                return null;

            empresaExiste.Nome = empresa.Nome;
            empresaExiste.Endereco = empresa.Endereco;
            

            await _context.SaveChangesAsync();

            return empresaExiste;
        }

        public async Task<EmpresasAssistencia> CriarEmpresaAssistenciaAsync(EmpresasAssistencia empresa)
        {
            await _context.EmpresasAssistencia.AddAsync(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<bool> DeletarEmpresaAssistenciaAsync(int id)
        {
            var empresaExiste = await _context.EmpresasAssistencia
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empresaExiste == null)
                return false;

            _context.EmpresasAssistencia.Remove(empresaExiste);
            await _context.SaveChangesAsync();
            return true;
        }
        

        public async Task<IEnumerable<EmpresasAssistencia>> ListarEmpresasAssistenciaAsync()
        {
            return await _context.EmpresasAssistencia.ToListAsync();
        }

        public async Task<EmpresasAssistencia?> ObterEmpresaAssistenciaPorIdAsync(int id)
        {
            return await _context.EmpresasAssistencia.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
