using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Repository
{
    public interface IVeiculoAssistenciasRepository
    {
        Task<IEnumerable<VeiculosAssistencia>> ListarVeiculoAssistenciasAsync();
        Task<VeiculosAssistencia?> ObterVeiculoAssistenciaPorIdAsync(int id);
        Task<VeiculosAssistencia> CriarVeiculoAssistenciaAsync(VeiculosAssistencia veiculo);
        Task<VeiculosAssistencia?> AtualizarVeiculoAssistenciaAsync(int id, VeiculosAssistencia veiculo);
        Task<bool> DeletarVeiculoAssistenciaAsync(int id);
        Task<bool> ExisteVinculoAsync(
    int veiculoId,
    int planoId);
    }
}
