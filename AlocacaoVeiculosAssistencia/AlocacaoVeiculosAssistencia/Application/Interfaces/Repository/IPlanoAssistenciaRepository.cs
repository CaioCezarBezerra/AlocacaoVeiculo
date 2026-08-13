using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Repository
{
    public interface IPlanoAssistenciaRepository
    {
        Task<IEnumerable<PlanosAssistencia>> ListarPlanosAssistenciaAsync();
        Task<PlanosAssistencia?> ObterPlanoAssistenciaPorIdAsync(int id);
        Task<PlanosAssistencia> CriarPlanoAssistenciaAsync(PlanosAssistencia plano);
        Task<PlanosAssistencia?> AtualizarPlanoAssistenciaAsync(int id, PlanosAssistencia plano);
        Task<bool> DeletarPlanoAssistenciaAsync(int id);
    }
}
