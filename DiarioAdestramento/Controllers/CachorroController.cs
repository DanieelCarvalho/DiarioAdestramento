using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class CachorroController : ControllerBase
{
    private readonly ICachorroService _cachorroService;

    public CachorroController(ICachorroService cachorroService)
    {
        _cachorroService = cachorroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CachorroResponseDTO>>> GetAll()
    {
        var cachorros = await _cachorroService.GetAllCachorrosAsync();

        return Ok(cachorros);
    }
    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CachorroResponseDTO>>> GetAllWithPagination([FromQuery] CachorrosParameters cachorrosParameters)
    {
        var (itens, metadata) = await _cachorroService.GetAllPagination(cachorrosParameters);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CachorroResponseDTO>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("ID deve ser um número positivo");

        var cachorro = await _cachorroService.GetCachorroByIdAsync(id);

        if (cachorro is null)
            return NotFound($"Cachorro com ID {id} não foi encontrado");
       

        return Ok(cachorro);
     }
    

    [HttpPost]
    public async Task<ActionResult<CachorroResponseDTO>> Create(CachorroCreatedDTO cachorroCreatedDTO)
    {
        if(cachorroCreatedDTO is null) 
            return BadRequest("Dados inválidos");
        
        var cachorroDTO = await _cachorroService.CreateCachorroAsync(cachorroCreatedDTO);

        return CreatedAtAction(nameof(GetById), new { id = cachorroDTO.Id }, cachorroDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CachorroCreatedDTO cachorroUpdatedDTO)
    {
        if (id != cachorroUpdatedDTO.Id || id <= 0)
            return BadRequest("Dados inválidos");

        if (cachorroUpdatedDTO is null)
            return BadRequest("Dados para atualização não podem ser nulos");

        var cachorroAtualizado = await _cachorroService.UpdateCachorroAsync(cachorroUpdatedDTO);

        if (cachorroAtualizado is null)
            return NotFound($"Cachorro com ID {id} não foi encontrado");
        

        return Ok(cachorroAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("ID deve ser um número positivo");

        var cachorroExcluidoDTO = await _cachorroService.DeleteCachorroAsync(id);

        if (cachorroExcluidoDTO is null)
            return NotFound($"Cachorro com ID {id} não foi encontrado");

        return Ok(cachorroExcluidoDTO);
    }

}
