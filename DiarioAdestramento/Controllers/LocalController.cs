using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class LocalController : ControllerBase
{
   
    private readonly ILocalService _localService;

    public LocalController(ILocalService localService)
    {
        
        _localService = localService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocalUpdateResponseDTO>>> GetAll()
    {
        var locais = await _localService.GetAllLocaisAsync();

        if (locais is null)
        {
            return NotFound("Nenhum local encontrado.");
        }
        

        return Ok(locais);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<LocalResponseDTO>>> GetAllWithPagination([FromQuery] LocalParameters locaisParameters)
    {


      var (itens, metadata) = await _localService.GetPaginateLocaisAsync(locaisParameters);

 
        Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
        return Ok(itens);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<LocalResponseDTO>> GetById(int id)
    {
        
        if (id <= 0 || id == null) return BadRequest("Id inválido.");

        var local = await _localService.GetByIdLocalAsync(id);


        if (local is null) return NotFound($"Local com id {id} não encontrado.");

      
        return Ok(local);
    }

    [HttpPost]
    public async Task<ActionResult<LocalCreatedDTO>> Post(LocalCreatedDTO localDTO)
    {
        if (localDTO is null) return BadRequest("Dados inválidos.");
        var local =await _localService.CreatedLocalAsync(localDTO);
       
        return CreatedAtAction(nameof(GetById), new { id = local.Id }, local);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<LocalResponseDTO>> Update(int id, LocalUpdateRequestDTO localDTO)
    {
        if (id <= 0 || id == null) return BadRequest("Id inválido.");
        if (localDTO is null) return BadRequest("Dados inválidos.");

        var local =  await _localService.UpdateLocalAsync(localDTO);
        if (local is null) return NotFound($"Local com Id {id} não foi encontrado");


        return Ok(local);

    }
   

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0 || id == null) return BadRequest("Id inválido.");
        var localDeletado = await _localService.DeleteLocalAsync(id);
        if (localDeletado is null) return NotFound($"Local com id {id} não encontrado.");

   
        return Ok(localDeletado);
    }


}
