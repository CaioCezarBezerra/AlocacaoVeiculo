using AlocacaoVeiculosAssistencia.Application.DTOs.Empresa;
using AlocacaoVeiculosAssistencia.Application.DTOs.GrupoVeiculos;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;

namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class GrupoVeiculoService : IGrupoVeiculoService
    {
        private readonly IGrupoVeiculosRepository _grupoVeiculoRepository;
        public GrupoVeiculoService(IGrupoVeiculosRepository grupoVeiculosRepository)
        {
            _grupoVeiculoRepository = grupoVeiculosRepository;
        }

        public async Task<GrupoVeiculosResponseDtos> AtualizarGrupoVeiculoAsync(int id, GruposVeiculosUpdateDtos dto)
        {
            var grupo = new GruposVeiculos
            {
                
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            var grupoAtualizado =
                await _grupoVeiculoRepository.AtualizarGrupoVeiculoAsync(id, grupo);

            if (grupoAtualizado == null)
                return null;

            return new GrupoVeiculosResponseDtos
            {
                
                Nome = grupoAtualizado.Nome,
                Descricao = grupoAtualizado.Descricao
            };
        }

        public async Task<GrupoVeiculosResponseDtos> CriarGrupoVeiculoAsync(GrupoVeiculosCreateDtos dto)
        {
            var grupo = new GruposVeiculos
            {
                
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            var grupoCriado =
                await _grupoVeiculoRepository.CriarGrupoVeiculoAsync(grupo);

            return new GrupoVeiculosResponseDtos
            {
               
                Nome = grupoCriado.Nome,
                Descricao = grupoCriado.Descricao
            };
        }

        public async Task<bool> DeletarGrupoVeiculoAsync(int id)
        {
            return await _grupoVeiculoRepository.DeletarGrupoVeiculoAsync(id);
        }

        public async Task<IEnumerable<GrupoVeiculosResponseDtos>> ListarGrupoVeiculosAsync()
        {
            var grupo = await _grupoVeiculoRepository.ListarGrupoVeiculosAsync();


            return grupo.Select(x => new GrupoVeiculosResponseDtos
            {
                Id = x.Id,
                Nome = x.Nome,
                Descricao = x.Descricao
            });
        }

        public async Task<GrupoVeiculosResponseDtos> ObterGrupoVeiculoPorIdAsync(int id)
        {
            

            var grupo =
                await _grupoVeiculoRepository.ObterGrupoVeiculoPorIdAsync(id);

            if (grupo == null)
                return null;

            return new GrupoVeiculosResponseDtos
            {
                Id = grupo.Id,
                Nome = grupo.Nome,
                Descricao = grupo.Descricao
            };
        }
    }
}
