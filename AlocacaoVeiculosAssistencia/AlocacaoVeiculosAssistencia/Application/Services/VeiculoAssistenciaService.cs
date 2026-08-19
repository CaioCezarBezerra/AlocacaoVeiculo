using AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia;
using AlocacaoVeiculosAssistencia.Application.Exceptions;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class VeiculoAssistenciaService : IVeiculoAssistenciaService
    {
        private readonly IVeiculoAssistenciasRepository _veiculoAssistenciaRepository;
        private readonly ILogger<VeiculoAssistenciaService> _logger;

        public VeiculoAssistenciaService(
            IVeiculoAssistenciasRepository veiculoAssistenciaRepository,
            ILogger<VeiculoAssistenciaService> logger)
        {
            _veiculoAssistenciaRepository = veiculoAssistenciaRepository;
            _logger = logger;
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
            var existeVinculo =
                await _veiculoAssistenciaRepository
                    .ExisteVinculoAsync(
                        dto.VeiculoId,
                        dto.PlanoId);

            if (existeVinculo)
            {
                _logger.LogWarning(
                    "Tentativa de criar vínculo duplicado. VeiculoId: {VeiculoId}, PlanoId: {PlanoId}",
                    dto.VeiculoId,
                    dto.PlanoId
                );

                throw new ConflictException(
                    "Este veículo já está vinculado a este plano.");
            }

            var vinculo = new VeiculosAssistencia
            {
                VeiculoId = dto.VeiculoId,
                PlanoId = dto.PlanoId
            };

            var vinculoCriado =
                await _veiculoAssistenciaRepository
                    .CriarVeiculoAssistenciaAsync(vinculo);

            var vinculoCompleto =
                await _veiculoAssistenciaRepository
                    .ObterVeiculoAssistenciaPorIdAsync(
                        vinculoCriado.Id);

            if (vinculoCompleto == null)
            {
                throw new InvalidOperationException(
                    "Não foi possível carregar o vínculo criado.");
            }

            _logger.LogInformation(
                "Vínculo criado. Id: {Id}, VeiculoId: {VeiculoId}, PlanoId: {PlanoId}",
                vinculoCompleto.Id,
                vinculoCompleto.VeiculoId,
                vinculoCompleto.PlanoId
            );

            return new VinculoVeiculoPlanoResponseDto
            {
                Id = vinculoCompleto.Id,

                VeiculoId = vinculoCompleto.VeiculoId,
                Veiculo =
                    vinculoCompleto.Veiculo?.Modelo
                    ?? string.Empty,

                PlanoId = vinculoCompleto.PlanoId,
                Plano =
                    vinculoCompleto.Plano?.Plano
                    ?? string.Empty
            };
        }


        public async Task<VinculoVeiculoPlanoResponseDto?> AtualizarVeiculoAssistenciaAsync(int id, VinculoVeiculoPlanoRequestDto dto)
        {
            var vinculoAtual =
                await _veiculoAssistenciaRepository
                    .ObterVeiculoAssistenciaPorIdAsync(id);

            if (vinculoAtual == null)
                return null;

            var existeVinculo =
                await _veiculoAssistenciaRepository
                    .ExisteVinculoAsync(
                        dto.VeiculoId,
                        dto.PlanoId);

            if (
                existeVinculo &&
                (
                    vinculoAtual.VeiculoId != dto.VeiculoId ||
                    vinculoAtual.PlanoId != dto.PlanoId
                )
            )
            {
                throw new ConflictException(
                    "Este veículo já está vinculado a este plano.");
            }

            var vinculo = new VeiculosAssistencia
            {
                VeiculoId = dto.VeiculoId,
                PlanoId = dto.PlanoId
            };

            var vinculoAtualizado =
                await _veiculoAssistenciaRepository
                    .AtualizarVeiculoAssistenciaAsync(
                        id,
                        vinculo);

            if (vinculoAtualizado == null)
                return null;

            var vinculoCompleto =
                await _veiculoAssistenciaRepository
                    .ObterVeiculoAssistenciaPorIdAsync(id);

            if (vinculoCompleto == null)
                return null;

            _logger.LogInformation(
                "Vínculo atualizado. Id: {Id}, VeiculoId: {VeiculoId}, PlanoId: {PlanoId}",
                id,
                dto.VeiculoId,
                dto.PlanoId
            );

            return new VinculoVeiculoPlanoResponseDto
            {
                Id = vinculoCompleto.Id,

                VeiculoId = vinculoCompleto.VeiculoId,
                Veiculo =
                    vinculoCompleto.Veiculo?.Modelo
                    ?? string.Empty,

                PlanoId = vinculoCompleto.PlanoId,
                Plano =
                    vinculoCompleto.Plano?.Plano
                    ?? string.Empty
            };
        }


        public async Task<bool>
            DeletarVeiculoAssistenciaAsync(int id)
        {
            var resultado =
                await _veiculoAssistenciaRepository
                    .DeletarVeiculoAssistenciaAsync(id);

            if (resultado)
            {
                _logger.LogInformation(
                    "Vínculo excluído. Id: {Id}",
                    id
                );
            }

            return resultado;
        }
    }
}