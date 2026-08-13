using AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class VeiculoAssistenciaService : IVeiculoAssistenciaService
    {
        private readonly IVeiculoAssistenciasRepository _veiculoAssistenciaRepository;

        public VeiculoAssistenciaService(
            IVeiculoAssistenciasRepository veiculoAssistenciaRepository)
        {
            _veiculoAssistenciaRepository = veiculoAssistenciaRepository;
        }


        public async Task<IEnumerable<VinculoVeiculoPlanoResponseDto>>
            ObterTodosVeiculosAssistenciaAsync()
        {
            var vinculos = await _veiculoAssistenciaRepository
                .ListarVeiculoAssistenciasAsync();

            return vinculos.Select(x => new VinculoVeiculoPlanoResponseDto
            {
                Id = x.Id,

                VeiculoId = x.VeiculoId,
                Veiculo = x.Veiculo?.Modelo ?? string.Empty,

                PlanoId = x.PlanoId,
                Plano = x.Plano?.Plano ?? string.Empty
            });
        }


        public async Task<VinculoVeiculoPlanoResponseDto?>
            ObterVeiculoAssistenciaPorIdAsync(int id)
        {
            var vinculo = await _veiculoAssistenciaRepository
                .ObterVeiculoAssistenciaPorIdAsync(id);

            if (vinculo == null)
                return null;

            return new VinculoVeiculoPlanoResponseDto
            {
                Id = vinculo.Id,

                VeiculoId = vinculo.VeiculoId,
                Veiculo = vinculo.Veiculo?.Modelo ?? string.Empty,

                PlanoId = vinculo.PlanoId,
                Plano = vinculo.Plano?.Plano ?? string.Empty
            };
        }


        public async Task<VinculoVeiculoPlanoResponseDto>
            CriarVeiculoAssistenciaAsync(
                VinculoVeiculoPlanoCreateDto dto)
        {
            var vinculo = new VeiculosAssistencia
            {
                VeiculoId = dto.VeiculoId,
                PlanoId = dto.PlanoId
            };

            var vinculoCriado = await _veiculoAssistenciaRepository
                .CriarVeiculoAssistenciaAsync(vinculo);

            // Busca novamente para carregar Veiculo e Plano
            var vinculoCompleto = await _veiculoAssistenciaRepository
                .ObterVeiculoAssistenciaPorIdAsync(vinculoCriado.Id);

            return new VinculoVeiculoPlanoResponseDto
            {
                Id = vinculoCompleto!.Id,

                VeiculoId = vinculoCompleto.VeiculoId,
                Veiculo = vinculoCompleto.Veiculo?.Modelo ?? string.Empty,

                PlanoId = vinculoCompleto.PlanoId,
                Plano = vinculoCompleto.Plano?.Plano ?? string.Empty
            };
        }


        public async Task<VinculoVeiculoPlanoResponseDto?>
            AtualizarVeiculoAssistenciaAsync(
                int id,
                VinculoVeiculoPlanoRequestDto dto)
        {
            var vinculo = new VeiculosAssistencia
            {
                VeiculoId = dto.VeiculoId,
                PlanoId = dto.PlanoId
            };

            var vinculoAtualizado = await _veiculoAssistenciaRepository
                .AtualizarVeiculoAssistenciaAsync(id, vinculo);

            if (vinculoAtualizado == null)
                return null;

            // Busca novamente para carregar os relacionamentos
            var vinculoCompleto = await _veiculoAssistenciaRepository
                .ObterVeiculoAssistenciaPorIdAsync(id);

            if (vinculoCompleto == null)
                return null;

            return new VinculoVeiculoPlanoResponseDto
            {
                Id = vinculoCompleto.Id,

                VeiculoId = vinculoCompleto.VeiculoId,
                Veiculo = vinculoCompleto.Veiculo?.Modelo ?? string.Empty,

                PlanoId = vinculoCompleto.PlanoId,
                Plano = vinculoCompleto.Plano?.Plano ?? string.Empty
            };
        }


        public async Task<bool> DeletarVeiculoAssistenciaAsync(int id)
        {
            return await _veiculoAssistenciaRepository
                .DeletarVeiculoAssistenciaAsync(id);
        }
    }
}