using AlocacaoVeiculosAssistencia.Application.DTOs.PlanosAssistencia;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AlocacaoVeiculosAssistencia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanosAssistenciasController : ControllerBase
    {

        private readonly IPlanoService _planoService;

        public PlanosAssistenciasController(IPlanoService planoService)
        {
            _planoService = planoService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanoResponseDtos>>> GetTodos()
        {
            var planos = await _planoService.ListarPlanosAsync();

            return Ok(planos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlanoResponseDtos>> ObterPlanoPorId(int id)
        {
            var plano = await _planoService.ObterPlanoPorIdAsync(id);

            if (plano == null)
            {
                return NotFound(new
                {
                    mensagem = "Plano de assistência não encontrado."
                });
            }

            return Ok(plano);
        }

        [HttpPost]
        public async Task<ActionResult<PlanoResponseDtos>> Criar([FromBody]
            PlanoCreateDtos dto)
        {
            var planos = await _planoService.CriarPlanoAsync(dto);

            return CreatedAtAction(nameof(ObterPlanoPorId),
            new { id = planos.Id },
            planos);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(
            int id,
             [FromBody] PlanoUpdateDtos dto)
        {

            var planoAtualizado = await _planoService.AtualizarPlanoAsync(id, dto);

            if (planoAtualizado == null)
            {
                return NotFound(new
                {
                    message = "Plano de assistência não encontrado."
                });
            }
            return Ok(planoAtualizado);

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var deletado = await _planoService.DeletarPlanoAsync(id);

            if (!deletado)
            {
                return NotFound(new
                {
                    message = "Plano de assistência não encontrado."
                });
            }
            return NoContent();

        }
    }
}
