using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Data.Repository
{
    public class VeiculosRepository : IVeiculoRepository
    {
        private readonly AppDbContext _context;

        public VeiculosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Veiculos>> ListarVeiculosAsync()
        {
            return await _context.Veiculos
                .AsNoTracking()
                .Include(x => x.Grupo)
                .ToListAsync();
        }

        public async Task<Veiculos?> ObterVeiculoPorIdAsync(int id)
        {
            return await _context.Veiculos
                .AsNoTracking()
                .Include(x => x.Grupo)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Veiculos> CriarVeiculoAsync(int id, Veiculos veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);

            await _context.SaveChangesAsync();

            return veiculo;
        }

        public async Task<Veiculos?> AtualizarVeiculoAsync(
            int id,
            Veiculos veiculo)
        {
            var veiculoExistente = await _context.Veiculos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (veiculoExistente == null)
                return null;

            veiculoExistente.Modelo = veiculo.Modelo;
            veiculoExistente.Placa = veiculo.Placa;
            veiculoExistente.GrupoId = veiculo.GrupoId;

            await _context.SaveChangesAsync();

            return veiculoExistente;
        }

        public async Task<bool> DeletarVeiculoAsync(int id)
        {
            var veiculo = await _context.Veiculos
                .FirstOrDefaultAsync(x => x.Id == id);

            if (veiculo == null)
                return false;

            _context.Veiculos.Remove(veiculo);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}