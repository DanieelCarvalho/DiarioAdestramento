using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Estatisticas;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class EstatisticaController : ControllerBase
{
    private readonly IEstatisticaRepository _repository;
    private string AdestradorId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public EstatisticaController(IEstatisticaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("cachorro/{cachorroId:int}/clima")]
    public async Task<ActionResult<IEnumerable<DesempenhoPorClimaDTO>>> GetPorClima(int cachorroId)
        => Ok(await _repository.GetDesempenhoPorClimaAsync(cachorroId, AdestradorId));

    [HttpGet("cachorro/{cachorroId:int}/local")]
    public async Task<ActionResult<IEnumerable<DesempenhoPorLocalDTO>>> GetPorLocal(int cachorroId)
    {
        var resultado = await _repository.GetDesempenhoPorLocalAsync(cachorroId, AdestradorId);
        return Ok(resultado);
    }


    [HttpGet("cachorro/{cachorroId:int}/comando/{comando}")]
    public async Task<ActionResult<IEnumerable<EvolucaoComandoDTO>>> GetPorComando(int cachorroId, string comando)
        => Ok(await _repository.GetEvolucaoPorComandoAsync(cachorroId, comando, AdestradorId));
}