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
    public class VeiculosController : ControllerBase
    {

         private readonly IVeiculoService _veiculosService;

         public VeiculosController(IVeiculoService veiculosService)
         {
             _veiculosService = veiculosService;
         }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculosResponseDtos>>> GetTodos()
        {
            var veiculos = await _veiculosService.ListarVeiculosAsync();

            return Ok(veiculos);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<VeiculosResponseDtos>> ObterPorId(int id)
        {
            var veiculo = await _veiculosService.ObterVeiculoPorIdAsync(id);

            if(veiculo == null)
            {
                return NotFound(new
                {
                    message = "Veículo não encontrado."
                });
            }

            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<ActionResult<VeiculosResponseDtos>> Criar(int id, [FromBody] VeiculosCreateDto dto)
        {
                var veiculoCriado = await _veiculosService.CriarVeiculoAsync(id, dto);

                return CreatedAtAction(nameof(ObterPorId),
                new { id = veiculoCriado.Id },
                veiculoCriado);
        }

            [HttpPut("{id:int}")]
            public async Task<IActionResult> Atualizar(int id, [FromBody] VeiculosUpdateDtos dto)
            {
                var veiculoAtualizado = await _veiculosService.AtualizarVeiculoAsync(id, dto);

            if(veiculoAtualizado == null)
            {
                return NotFound(new
                {
                    message = "Veículo não encontrado."
                });
            }
            return Ok(veiculoAtualizado);

        }


            [HttpDelete("{id:int}")]
            public async Task<IActionResult> Excluir(int id)
            {
                var veiculoExcluido = await _veiculosService.DeletarVeiculoAsync(id);

                if(veiculoExcluido == null)
                {
                    return NotFound(new
                    {
                        message = "Veículo não encontrado."
                    });
                }

                return Ok(veiculoExcluido);
            }


        }
    
}
