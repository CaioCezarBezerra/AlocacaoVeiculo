using AlocacaoVeiculosAssistencia.Application.DTOs.Veiculos;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Repository;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;


namespace AlocacaoVeiculosAssistencia.Application.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IGrupoVeiculosRepository _grupoVeiculosRepository;

        public VeiculoService(IVeiculoRepository veiculoRepository, IGrupoVeiculosRepository grupoVeiculosRepository    )
        {
            _veiculoRepository = veiculoRepository;
            _grupoVeiculosRepository = grupoVeiculosRepository;
        }

        public async Task<IEnumerable<VeiculosResponseDtos>> ListarVeiculosAsync()
        {
            var veiculos = await _veiculoRepository.ListarVeiculosAsync();


            return veiculos.Select(x => new VeiculosResponseDtos
            {
                Id = x.Id,
                Modelo = x.Modelo,
                Placa = x.Placa,
                GrupoId = x.GrupoId,
                GrupoNome = x.Grupo.Nome,
            });
        }

        public async Task<VeiculosResponseDtos?> ObterVeiculoPorIdAsync(int id)
        {
            var veiculo = await _veiculoRepository.ObterVeiculoPorIdAsync(id);

            if (veiculo == null)
                return null;

            return new VeiculosResponseDtos
            {
                Id = veiculo.Id,
                Modelo = veiculo.Modelo,
                Placa = veiculo.Placa,
                GrupoId = veiculo.GrupoId,
                GrupoNome = veiculo.Grupo.Nome
            };
        }

        public async Task<VeiculosResponseDtos> CriarVeiculoAsync( int id, VeiculosCreateDto dto)
        {
            var grupo = await _grupoVeiculosRepository
        .ObterGrupoVeiculoPorIdAsync(dto.GrupoId);

            if (grupo == null)
            {
                throw new KeyNotFoundException(
                    "Grupo de veículos não encontrado.");
            }
            var veiculo = new Veiculos
            {
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                GrupoId = dto.GrupoId
            };

            var veiculoCriado =
                await _veiculoRepository.CriarVeiculoAsync(id, veiculo);

            return new VeiculosResponseDtos
            {
                Id = veiculoCriado.Id,
                Modelo = veiculoCriado.Modelo,
                Placa = veiculoCriado.Placa,
                GrupoId = veiculoCriado.GrupoId,
                GrupoNome = grupo.Nome

            };
        }

        public async Task<VeiculosResponseDtos?> AtualizarVeiculoAsync(
            int id,
            VeiculosUpdateDtos dto)
        {
            var veiculo = new Veiculos
            {
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                GrupoId = dto.GrupoId
            };

            var veiculoAtualizado =
                await _veiculoRepository.AtualizarVeiculoAsync(id, veiculo);

            if (veiculoAtualizado == null)
                return null;

            return new VeiculosResponseDtos
            {
                Id = veiculoAtualizado.Id,
                Modelo = veiculoAtualizado.Modelo,
                Placa = veiculoAtualizado.Placa,
                GrupoId = veiculoAtualizado.GrupoId
            };
        }

        public async Task<bool> DeletarVeiculoAsync(int id)
        {
            return await _veiculoRepository.DeletarVeiculoAsync(id);
        }
    }
}