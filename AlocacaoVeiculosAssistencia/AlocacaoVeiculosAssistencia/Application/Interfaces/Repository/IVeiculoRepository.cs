using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Repository
{
    public interface IVeiculoRepository
    {
        Task<IEnumerable<Veiculos>> ListarVeiculosAsync();
        Task<Veiculos?> ObterVeiculoPorIdAsync(int id);
        Task<Veiculos> CriarVeiculoAsync(int id, Veiculos veiculo);
        Task<Veiculos?> AtualizarVeiculoAsync(int id, Veiculos veiculo);
        Task<bool> DeletarVeiculoAsync(int id);
    }
}
