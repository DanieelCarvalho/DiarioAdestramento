using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalController : ControllerBase
{
    private readonly ILocalRepository _repository;

    public LocalController(ILocalRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LocalUpdateResponseDTO>>> GetAll()
    {
        var locais = await _repository.GetAllAsync();

        if (locais is null)
        {
            return NotFound("Nenhum local encontrado.");
        }
        var locaisDTO = locais.ToLocalResponseDTOList();

        return Ok(locaisDTO);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<LocalResponseDTO>>> GetAllWithPagination([FromQuery] LocalParameters locaisParameters)
    {


        var locais = await _repository.GetLocaisAsync(locaisParameters);


        var locaisDTO = locais.ToLocalResponseDTOList();
        var metadata = new
        {
            locais.TotalCount,
            locais.PageSize,
            locais.CurrentPage,
            locais.TotalPages,
            locais.HasNext,
            locais.HasPrevious
        };
        Response.Headers.Append("X-Pagination", System.Text.Json.JsonSerializer.Serialize(metadata));
        return Ok(locaisDTO);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<LocalResponseDTO>> GetById(int id)
    {
        var local = await _repository.GetAsync(l => l.Id == id);

        if (id <= 0 || id == null) return BadRequest("Id inválido.");

        if (local is null) return NotFound($"Local com id {id} não encontrado.");

        var localDTO = local.ToLocalResponseDTO();
        return Ok(localDTO);
    }

    [HttpPost]
    public async Task<ActionResult<LocalCreatedDTO>> Post(LocalCreatedDTO localDTO)
    {
        if (localDTO is null) return BadRequest("Local inválido.");
        var local = localDTO.ToLocal();
        await _repository.AddAsync(local);
        var createdLocalDTO = local.ToLocalCreatedDTO();
        return CreatedAtAction(nameof(GetById), new { id = local.Id }, createdLocalDTO);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, LocalUpdateRequestDTO localDTO)
    {
        if (id <= 0 || id == null) return BadRequest("Id inválido.");
        if (localDTO is null) return BadRequest("Local inválido.");

        var local = localDTO.ToLocalUpdadte();

        var localAtualizado = await _repository.UpdateAsync(local);

        var localAtualizadoDTO = localAtualizado.ToLocalResponseDTO();

        return Ok(localAtualizadoDTO);

    }
   

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0 || id == null) return BadRequest("Id inválido.");
        var local = await _repository.GetAsync(l => l.Id == id);
        if (local is null) return NotFound($"Local com id {id} não encontrado.");

        var categriaExcluida = await _repository.DeleteAsync(local);

        var localExcluidoDTO = categriaExcluida.ToLocalResponseDTO();
        return Ok(localExcluidoDTO);
    }


}
