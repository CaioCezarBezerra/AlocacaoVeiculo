using AlocacaoVeiculosAssistencia.Application.DTOs.Empresa;
using AlocacaoVeiculosAssistencia.Application.DTOs.Veiculos;
using AlocacaoVeiculosAssistencia.Application.DTOs.VeiculosAssistencia;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosAssistenciasController : ControllerBase
    {
        private readonly IVeiculoAssistenciaService _veiculoAssistenciaService;

        public VeiculosAssistenciasController(IVeiculoAssistenciaService veiculoAssistenciaService)
        {
            _veiculoAssistenciaService = veiculoAssistenciaService;
        }

        [Route("ListarVinculoVeiculo")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VinculoVeiculoPlanoResponseDto>>> GetTodos()
        {
           var veiculosAssistencia = await _veiculoAssistenciaService.ObterTodosVeiculosAssistenciaAsync();
            return Ok(veiculosAssistencia);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<VinculoVeiculoPlanoResponseDto>> ObterVeiculoAssistenciaPorId(int id)
        {
            var veiculoAssistencia = await _veiculoAssistenciaService.ObterVeiculoAssistenciaPorIdAsync(id);

            if (veiculoAssistencia == null)
            {
                return NotFound(new
                {
                    message = "Vínculo de veículo e plano não encontrado."
                });
            }

            return Ok(veiculoAssistencia);
        }

        [Route("CriarVinculos")]
        [HttpPost]
        public async Task<ActionResult<VinculoVeiculoPlanoResponseDto>> Criar(
     [FromBody] VinculoVeiculoPlanoCreateDto dto)
        {
            var veiculoAssistencia =
                await _veiculoAssistenciaService
                    .CriarVeiculoAssistenciaAsync(dto);

            return CreatedAtAction(
                nameof(ObterVeiculoAssistenciaPorId),
                new { id = veiculoAssistencia.Id },
                veiculoAssistencia);
        }
        [Route("AtualizarVinculoVeiculo/{id}")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(
            int id,
            [FromBody] VinculoVeiculoPlanoRequestDto dto)
        {
            var veiculoAssistencia = await _veiculoAssistenciaService.ObterVeiculoAssistenciaPorIdAsync(id);
            if (veiculoAssistencia == null)
            {
                return NotFound(new
                {
                    message = "Vínculo de veículo e plano não encontrado."
                });
            }
            return Ok(veiculoAssistencia);

        }

        [Route("DeletarVinculoVeiculo/{id}")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var deletarVeiculoAssistencia = await _veiculoAssistenciaService.DeletarVeiculoAssistenciaAsync(id);

            if (!deletarVeiculoAssistencia)
            {
                return NotFound(new
                {
                    message = "Vínculo de veículo e plano não encontrado."
                });
            }

            return NoContent();
        }


    }


}
