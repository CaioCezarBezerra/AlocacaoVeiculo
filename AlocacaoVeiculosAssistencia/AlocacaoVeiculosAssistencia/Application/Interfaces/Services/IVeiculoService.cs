using AlocacaoVeiculosAssistencia.Application.DTOs.Veiculos;
using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Interfaces.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<VeiculosResponseDtos>> ListarVeiculosAsync();
        Task<VeiculosResponseDtos?> ObterVeiculoPorIdAsync(int id);
        Task<VeiculosResponseDtos> CriarVeiculoAsync(int id, VeiculosCreateDto dto);
        Task<VeiculosResponseDtos?> AtualizarVeiculoAsync(int id, VeiculosUpdateDtos dto);
        Task<bool> DeletarVeiculoAsync(int id);
    }
}
