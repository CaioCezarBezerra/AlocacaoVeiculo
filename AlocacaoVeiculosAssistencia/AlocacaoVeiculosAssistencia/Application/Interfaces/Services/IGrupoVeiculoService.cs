using AlocacaoVeiculosAssistencia.Application.DTOs.GrupoVeiculos;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Services
{
    public interface IGrupoVeiculoService
    {
        Task<IEnumerable<GrupoVeiculosResponseDtos>> ListarGrupoVeiculosAsync();
        Task<GrupoVeiculosResponseDtos> ObterGrupoVeiculoPorIdAsync(int id);
        Task<GrupoVeiculosResponseDtos> CriarGrupoVeiculoAsync(GrupoVeiculosCreateDtos grupoVeiculos);
        Task<GrupoVeiculosResponseDtos> AtualizarGrupoVeiculoAsync(int id, GruposVeiculosUpdateDtos grupoVeiculos);
        Task<bool> DeletarGrupoVeiculoAsync(int id);
    }
}
