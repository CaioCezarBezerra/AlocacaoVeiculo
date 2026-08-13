using AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Services
{
    public interface IVeiculoAssistenciaService
    {
        Task<IEnumerable<VinculoVeiculoPlanoResponseDto>> ObterTodosVeiculosAssistenciaAsync();
        Task<VinculoVeiculoPlanoResponseDto> ObterVeiculoAssistenciaPorIdAsync(int id);
        Task<VinculoVeiculoPlanoResponseDto> CriarVeiculoAssistenciaAsync(VinculoVeiculoPlanoCreateDto dto);
        Task<VinculoVeiculoPlanoResponseDto> AtualizarVeiculoAssistenciaAsync(int id, VinculoVeiculoPlanoRequestDto dto);
        Task<bool> DeletarVeiculoAssistenciaAsync(int id);
    }
}
