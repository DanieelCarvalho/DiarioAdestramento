using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class CachorroController : ControllerBase
{
    private readonly ICachorroService _cachorroService;
    private string AdestradorId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public CachorroController(ICachorroService cachorroService)
    {
        _cachorroService = cachorroService;
    }

    [HttpGet]
    
    public async Task<ActionResult<IEnumerable<CachorroResponseDTO>>> GetAll()
    {
        var cachorros = await _cachorroService.GetAllCachorrosAsync(AdestradorId);

        return Ok(cachorros);
    }

 

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<CachorroResponseDTO>>> GetAllWithPagination([FromQuery] CachorrosParameters cachorrosParameters)
    {
        var (itens, metadata) = await _cachorroService.GetAllPagination(cachorrosParameters, AdestradorId);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

        return Ok(itens);
    }

    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<CachorroResponseDTO>> GetById(int id)
    {
        var cachorro = await _cachorroService.GetCachorroByIdAsync(id, AdestradorId);
        
        return Ok(cachorro);
     }
    

    [HttpPost]
    public async Task<ActionResult<CachorroResponseDTO>> Create(CachorroCreatedDTO cachorroCreatedDTO)
    {
      
        var cachorroDTO = await _cachorroService.CreateCachorroAsync(cachorroCreatedDTO, AdestradorId);

        return CreatedAtAction(nameof(GetById), new { id = cachorroDTO.Id }, cachorroDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CachorroCreatedDTO cachorroUpdatedDTO)
    {
        cachorroUpdatedDTO.Id = id;
        var cachorroAtualizado = await _cachorroService.UpdateCachorroAsync(cachorroUpdatedDTO, AdestradorId);

        return Ok(cachorroAtualizado);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var cachorroExcluidoDTO = await _cachorroService.DeleteCachorroAsync(id, AdestradorId);
        
        return Ok(cachorroExcluidoDTO);
    }

    [HttpPut("{id:int}/foto")]
    [RequestSizeLimit(3 * 1024 * 1024)]
    public async Task<IActionResult> UploadFoto(int id, IFormFile arquivo)
    {
        await using var stream = arquivo.OpenReadStream(); 
        await _cachorroService.SalvarFotoAsync(id, stream, arquivo.Length, AdestradorId);
         return NoContent();
    }

    [HttpGet("{id:int}/foto")]
    public async Task<IActionResult> GetFoto(int id)
    {
        var (foto, tipo) = await _cachorroService.ObterFotoAsync(id, AdestradorId);
        return  File(foto, tipo);
    }

    [HttpDelete("{id:int}/foto")]
    public async Task<IActionResult> DeleteFoto(int id)
    {
        await _cachorroService.ExcluirFotoAsync(id, AdestradorId);
        return NoContent();
    }
      

}
