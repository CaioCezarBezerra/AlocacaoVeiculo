using AlocacaoVeiculosAssistencia.Application.DTOs.PlanosAssistencia;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Services
{
    public interface IPlanoService
    {
        Task<IEnumerable<PlanoResponseDtos>> ListarPlanosAsync();
        Task<PlanoResponseDtos?> ObterPlanoPorIdAsync(int id);
        Task<PlanoResponseDtos> CriarPlanoAsync(PlanoCreateDtos dto);
        Task<PlanoResponseDtos?> AtualizarPlanoAsync(int id, PlanoUpdateDtos dto);
        Task<bool> DeletarPlanoAsync(int id);
    }
}
