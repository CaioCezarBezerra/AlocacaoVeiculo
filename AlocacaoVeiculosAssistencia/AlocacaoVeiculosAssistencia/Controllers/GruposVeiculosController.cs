using AlocacaoVeiculosAssistencia.Application.DTOs.GrupoVeiculos;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GruposVeiculosController : ControllerBase
{
    private readonly IGrupoVeiculoService _grupoVeiculoService;

    public GruposVeiculosController(IGrupoVeiculoService grupoVeiculoService)
    {
        _grupoVeiculoService = grupoVeiculoService;
    }

    [Route("ListarGruposVeiculos")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GrupoVeiculosResponseDtos>>> GetTodos()
    {
        var grupos = await _grupoVeiculoService.ListarGrupoVeiculosAsync();
        return Ok(grupos);
    }

    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GrupoVeiculosResponseDtos>> ObterGrupoVeiculoPorId(int id)
    {
        var grupo = await _grupoVeiculoService.ObterGrupoVeiculoPorIdAsync(id);

        if (grupo == null)
        {
            return NotFound(new
            {
                mensagem = "Grupo de veículos não encontrado."
            });
        }

        return Ok(grupo);
    }
    [Route("CriarGruposVeiculos")]      
    [HttpPost]
    public async Task<ActionResult<GrupoVeiculosResponseDtos>> Criar(
        GrupoVeiculosCreateDtos dto)
    {
        var grupoCriado = await _grupoVeiculoService.CriarGrupoVeiculoAsync(dto);

        return CreatedAtAction(nameof(ObterGrupoVeiculoPorId),
        new { id = grupoCriado.Id },
        grupoCriado);
    }

    [Route("AtualizarGrupoVeiculos/{id}")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] GruposVeiculosUpdateDtos dto)
    {
        var grupoAtualizado = await _grupoVeiculoService.AtualizarGrupoVeiculoAsync(id, dto);

        if (grupoAtualizado == null)
        {
            return NotFound(new
            {
                mensagem = "Grupo de veículos não encontrado."
            });
        }

        return Ok(grupoAtualizado);
    }
    [Route("DeletarGrupoVeiculos/{id}")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var deletado = await _grupoVeiculoService.DeletarGrupoVeiculoAsync(id);

        if (!deletado)
        {
            return NotFound(new
            {
                message = "Grupo de veículos não encontrado."
            });
        }

        return NoContent();
    }
}