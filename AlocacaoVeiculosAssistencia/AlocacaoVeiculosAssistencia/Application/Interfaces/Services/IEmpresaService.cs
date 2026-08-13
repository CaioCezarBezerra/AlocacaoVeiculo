using AlocacaoVeiculosAssistencia.Application.DTOs.Empresa;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Services
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaAssistenciaResponseDtos>> ListarEmpresasAsync();
        Task<EmpresaAssistenciaResponseDtos?> ObterEmpresaPorIdAsync(int id);
        Task<EmpresaAssistenciaResponseDtos> CriarEmpresaAsync(EmpresaAssistenciaCreateDtos dto);
        Task<EmpresaAssistenciaResponseDtos?> AtualizarEmpresaAsync(int id, EmpresaAssistenciaUpdateDtos dto);
        Task<bool> DeletarEmpresaAsync(int id);
    }
}
