using AlocacaoVeiculosAssistencia.Application.DTOs.PlanosAssistencia;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class PlanoAssistenciaService : IPlanoService
    {
        private readonly IPlanoAssistenciaRepository _planoRepository;
        private readonly IEmpresasAssistenciaRepository _empresaRepository;

        public PlanoAssistenciaService(
            IPlanoAssistenciaRepository planoRepository,
            IEmpresasAssistenciaRepository empresaRepository)
        {
            _planoRepository = planoRepository;
            _empresaRepository = empresaRepository;
        }


        public async Task<IEnumerable<PlanoResponseDtos>>
            ListarPlanosAsync()
        {
            var planos = await _planoRepository
                .ListarPlanosAssistenciaAsync();

            return planos.Select(x => new PlanoResponseDtos
            {
                Id = x.Id,
                Plano = x.Plano,
                Descricao = x.Descricao,
                ValorCobertura = x.ValorCobertura,
                EmpresaId = x.EmpresaId,
                EmpresaNome = x.Empresa?.Nome ?? string.Empty
            });
        }


        public async Task<PlanoResponseDtos?>
            ObterPlanoPorIdAsync(int id)
        {
            var plano = await _planoRepository
                .ObterPlanoAssistenciaPorIdAsync(id);

            if (plano == null)
                return null;

            return new PlanoResponseDtos
            {
                Id = plano.Id,
                Plano = plano.Plano,
                Descricao = plano.Descricao,
                ValorCobertura = plano.ValorCobertura,
                EmpresaId = plano.EmpresaId,
                EmpresaNome = plano.Empresa?.Nome ?? string.Empty
            };
        }


        public async Task<PlanoResponseDtos>
            CriarPlanoAsync(PlanoCreateDtos dto)
        {
            var empresa = await _empresaRepository
                .ObterEmpresaAssistenciaPorIdAsync(dto.EmpresaId);

            if (empresa == null)
            {
                throw new KeyNotFoundException(
                    "Empresa de assistência não encontrada.");
            }

            var plano = new PlanosAssistencia
            {
                EmpresaId = dto.EmpresaId,
                Plano = dto.Plano,
                Descricao = dto.Descricao,
                ValorCobertura = dto.ValorCobertura
            };

            var planoCriado = await _planoRepository
                .CriarPlanoAssistenciaAsync(plano);

            return new PlanoResponseDtos
            {
                Id = planoCriado.Id,
                Plano = planoCriado.Plano,
                Descricao = planoCriado.Descricao,
                ValorCobertura = planoCriado.ValorCobertura,
                EmpresaId = empresa.Id,
                EmpresaNome = empresa.Nome
            };
        }


        public async Task<PlanoResponseDtos?>
            AtualizarPlanoAsync(
                int id,
                PlanoUpdateDtos dto)
        {
            var empresa = await _empresaRepository
                .ObterEmpresaAssistenciaPorIdAsync(dto.EmpresaId);

            if (empresa == null)
            {
                throw new KeyNotFoundException(
                    "Empresa de assistência não encontrada.");
            }

            var plano = new PlanosAssistencia
            {
                Plano = dto.Plano,
                Descricao = dto.Descricao,
                ValorCobertura = dto.ValorCobertura,
                EmpresaId = dto.EmpresaId
            };

            var planoAtualizado = await _planoRepository
                .AtualizarPlanoAssistenciaAsync(id, plano);

            if (planoAtualizado == null)
                return null;

            return new PlanoResponseDtos
            {
                Id = planoAtualizado.Id,
                Plano = planoAtualizado.Plano,
                Descricao = planoAtualizado.Descricao,
                ValorCobertura = planoAtualizado.ValorCobertura,
                EmpresaId = planoAtualizado.EmpresaId,
                EmpresaNome = empresa.Nome
            };
        }


        public async Task<bool> DeletarPlanoAsync(int id)
        {
            return await _planoRepository
                .DeletarPlanoAssistenciaAsync(id);
        }
    }
}