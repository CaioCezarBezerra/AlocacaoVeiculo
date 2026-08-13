namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Repository
{
    public interface IEmpresasAssistenciaRepository
    {
        Task<IEnumerable<EmpresasAssistencia>> ListarEmpresasAssistenciaAsync();
        Task<EmpresasAssistencia?> ObterEmpresaAssistenciaPorIdAsync(int id);
        Task<EmpresasAssistencia> CriarEmpresaAssistenciaAsync(EmpresasAssistencia empresa);
        Task<EmpresasAssistencia?> AtualizarEmpresaAssistenciaAsync(int id, EmpresasAssistencia empresa);
        Task<bool> DeletarEmpresaAssistenciaAsync(int id);
    }
}
