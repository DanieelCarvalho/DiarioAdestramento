using DiarioAdestramento.Dtos;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class SessaoTreinoController : ControllerBase
{
    private readonly ISessaoTreinoService _sessaoTreinoService;

    public SessaoTreinoController(ISessaoTreinoService sessaoTreinoService)
    {
        _sessaoTreinoService = sessaoTreinoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessaoTreinoResponseDTO>>> GetAll([FromQuery] SessoesParameters parametros)
    {
        var (itens, metadata) = await _sessaoTreinoService.GetAllSessoesAsync(parametros);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SessaoTreinoResponseDTO>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido.");

        var sessao = await _sessaoTreinoService.GetSessaoByIdAsync(id);
        if (sessao is null)
            return NotFound($"Sessão com ID {id} não foi encontrada.");

        return Ok(sessao);
    }

    [HttpGet("/api/cachorro/{cachorroId:int}/sessoes")]
    public async Task<ActionResult<CachorroComSessoesResponseDTO>> GetByCachorroId(
        int cachorroId, [FromQuery] SessoesParameters parametros)
    {
        var (cachorro, metadata) = await _sessaoTreinoService.GetSessoesByCachorroIdAsync(cachorroId, parametros);
        if (cachorro is null)
            return NotFound($"Cachorro com ID {cachorroId} não foi encontrado.");

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
        return Ok(cachorro);
    }

    [HttpPost]
    public async Task<ActionResult<SessaoTreinoResponseDTO>> Create(CriarSessaoTreinoDto dto)
    {
        if (dto is null)
            return BadRequest("Dados inválidos.");

        var (sessao, erro) = await _sessaoTreinoService.CreateSessaoAsync(dto);
        if (erro is not null)
            return BadRequest(erro);

        return CreatedAtAction(nameof(GetById), new { id = sessao!.Id }, sessao);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SessaoTreinoResponseDTO>> Update(int id, UpdateSessaoTreinoDTO dto)
    {
        if (id <= 0)
            return BadRequest("Id inválido.");
        if (dto is null)
            return BadRequest("Dados inválidos.");

        var sessaoAtualizada = await _sessaoTreinoService.UpdateSessaoAsync(id, dto);
        if (sessaoAtualizada is null)
            return NotFound($"Sessão com ID {id} não foi encontrada.");

        return Ok(sessaoAtualizada);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<SessaoTreinoResponseDTO>> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("Id inválido.");

        var sessaoExcluida = await _sessaoTreinoService.DeleteSessaoAsync(id);
        if (sessaoExcluida is null)
            return NotFound($"Sessão com ID {id} não foi encontrada.");

        return Ok(sessaoExcluida);
    }
}