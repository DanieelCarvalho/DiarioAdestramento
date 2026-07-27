// Controllers/EstatisticaController.cs
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EstatisticaController : ControllerBase
{
    private readonly IEstatisticaRepository _repository;

    public EstatisticaController(IEstatisticaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("cachorro/{cachorroId:int}/clima")]
    public async Task<ActionResult<IEnumerable<DesempenhoPorClimaDTO>>> GetPorClima(int cachorroId)
        => Ok(await _repository.GetDesempenhoPorClimaAsync(cachorroId));

    //[HttpGet("cachorro/{cachorroId:int}/local")]
    //public async Task<ActionResult<IEnumerable<DesempenhoPorLocalDTO>>> GetPorLocal(int cachorroId)
    //    => Ok(await _estatisticaService.GetDesempenhoPorLocalAsync(cachorroId));

    [HttpGet("cachorro/{cachorroId:int}/comando/{comando}")]
    public async Task<ActionResult<IEnumerable<EvolucaoComandoDTO>>> GetPorComando(int cachorroId, string comando)
        => Ok(await _repository.GetEvolucaoPorComandoAsync(cachorroId, comando));
}