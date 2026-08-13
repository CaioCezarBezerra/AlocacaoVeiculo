using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Repository
{
    public interface IGrupoVeiculosRepository
    {
        Task<IEnumerable<GruposVeiculos>> ListarGrupoVeiculosAsync();
        Task<GruposVeiculos> ObterGrupoVeiculoPorIdAsync(int id);
        Task<GruposVeiculos> CriarGrupoVeiculoAsync(GruposVeiculos grupoVeiculos);
        Task<GruposVeiculos> AtualizarGrupoVeiculoAsync(int id, GruposVeiculos veiculos);
        Task<bool> DeletarGrupoVeiculoAsync(int id);
    }
}
