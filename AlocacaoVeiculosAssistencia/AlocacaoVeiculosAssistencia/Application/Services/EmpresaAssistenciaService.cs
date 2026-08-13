using AlocacaoVeiculosAssistencia.Application.DTOs.Empresa;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;

namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class EmpresaAssistenciaService : IEmpresaService
    {
        private readonly IEmpresasAssistenciaRepository _empresaRepository;

        public EmpresaAssistenciaService(
            IEmpresasAssistenciaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<EmpresaAssistenciaResponseDtos>>ListarEmpresasAsync()
        {
            var empresas =
                await _empresaRepository.ListarEmpresasAssistenciaAsync();

            return empresas.Select(x => new EmpresaAssistenciaResponseDtos
            {
                Id = x.Id,
                Nome = x.Nome,
                Endereco = x.Endereco
            });
        }

        public async Task<EmpresaAssistenciaResponseDtos?>
            ObterEmpresaPorIdAsync(int id)
        {
            var empresa =
                await _empresaRepository.ObterEmpresaAssistenciaPorIdAsync(id);

            if (empresa == null)
                return null;

            return new EmpresaAssistenciaResponseDtos
            {
                Id = empresa.Id,
                Nome = empresa.Nome,
                Endereco = empresa.Endereco
            };
        }

        public async Task<EmpresaAssistenciaResponseDtos>
            CriarEmpresaAsync(EmpresaAssistenciaCreateDtos dto)
        {
            var empresa = new EmpresasAssistencia
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco
            };

            var empresaCriada =
                await _empresaRepository.CriarEmpresaAssistenciaAsync(empresa);

            return new EmpresaAssistenciaResponseDtos
            {
                Id = empresaCriada.Id,
                Nome = empresaCriada.Nome,
                Endereco = empresaCriada.Endereco
            };
        }

        public async Task<EmpresaAssistenciaResponseDtos?>
            AtualizarEmpresaAsync(
                int id,
                EmpresaAssistenciaUpdateDtos dto)
        {
            var empresa = new EmpresasAssistencia
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco
            };

            var empresaAtualizada =
                await _empresaRepository.AtualizarEmpresaAssistenciaAsync(
                    id,
                    empresa);

            if (empresaAtualizada == null)
                return null;

            return new EmpresaAssistenciaResponseDtos
            {
                Id = empresaAtualizada.Id,
                Nome = empresaAtualizada.Nome,
                Endereco = empresaAtualizada.Endereco
            };
        }

        public async Task<bool> DeletarEmpresaAsync(int id)
        {
            return await _empresaRepository
                .DeletarEmpresaAssistenciaAsync(id);
        }
    }
}