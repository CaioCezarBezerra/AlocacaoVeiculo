using AlocacaoVeiculosAssistencia.Application.DTOs.Empresa;
using AlocacaoVeiculosAssistencia.Application.Interfaces.Services;
using AlocacaoVeiculosAssistencia.Application.Services;
using AlocacaoVeiculosAssistencia.Domain.Entities;
using AlocacaoVeiculosAssistencia.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlocacaoVeiculosAssistencia.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasAssistenciaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresasAssistenciaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaAssistenciaResponseDtos>>> GetTodos()
    {
        var empresas = await _empresaService.ListarEmpresasAsync();

        return Ok(empresas);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmpresaAssistenciaResponseDtos>> ObterEmpresaPorId(int id)
    {
        var empresas = await _empresaService.ObterEmpresaPorIdAsync(id);

        if (empresas == null)
        {
            return NotFound(new
            {
                message = "Empresa não encontrada."
            });
        }

        return Ok(empresas);
    }

    [HttpPost]
    public async Task<ActionResult<EmpresaAssistenciaResponseDtos>> Criar([FromBody]
        EmpresaAssistenciaCreateDtos dto)
    {
        var empresas = await _empresaService.CriarEmpresaAsync(dto);

        return CreatedAtAction(nameof(ObterEmpresaPorId),
        new { id = empresas.Id },
        empresas);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(
        int id, [FromBody] EmpresaAssistenciaUpdateDtos dto)
    {
        var empresaAtualizada = await _empresaService.AtualizarEmpresaAsync(id, dto);

        if (empresaAtualizada == null)
        {
            return NotFound(new
            {
                message = "Empresa não encontrada."
            });
        }
        return Ok(empresaAtualizada);
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var deletado = await _empresaService.DeletarEmpresaAsync(id);

        if (!deletado)
        {
            return NotFound(new
            {
                message = "Empresa não encontrada."
            });
        }

        return NoContent();
    }


}
