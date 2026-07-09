using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CachorroController : ControllerBase
{
    private readonly ICachorroRepository _cachorroRepository;

    public CachorroController(ICachorroRepository cachorroRepository)
    {
        _cachorroRepository = cachorroRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CachorroResponseDTO>>> GetAll()
    {
        var cachorros = await _cachorroRepository.GetAllAsync();
      
        var cachorrosDTO = cachorros.ToCachorroResponseDTOList();

        return Ok(cachorrosDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CachorroResponseDTO>> GetById(int id)
    {
        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == id);
        if (cachorro == null)
        {
            return NotFound();
        }
        var cachorroDTO = cachorro.ToCachorroResponseDTO();
        return Ok(cachorroDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CachorroResponseDTO>> Create(CachorroCreatedDTO cachorroCreatedDTO)
    {
        if(cachorroCreatedDTO is null)
        {
            return BadRequest();
        }
        var cachorro = cachorroCreatedDTO.ToCachorro();
        await _cachorroRepository.AddAsync(cachorro);
        var cachorroDTO = cachorro.ToCachorroResponseDTO();
        return CreatedAtAction(nameof(GetById), new { id = cachorroDTO.Id }, cachorroDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CachorroCreatedDTO cachorroUpdatedDTO)
    {
        if (id != cachorroUpdatedDTO.Id || id <= 0)
        {
            return BadRequest();
        }
        if (cachorroUpdatedDTO is null)
        {
            return NotFound();
        }

        var cachorro = cachorroUpdatedDTO.ToCachorro();

        var cachorroExiste = await _cachorroRepository.UpdateAsync(cachorro);

        var cachorroAtualizado = cachorroExiste.ToCachorroResponseDTO();


        return Ok(cachorroAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == id);
        if (cachorro == null)
        {
            return NotFound();
        }
       var cachorroExcluido =  await _cachorroRepository.DeleteAsync(cachorro);
       var cachorroExcluidoDTO = cachorroExcluido.ToCachorroResponseDTO();
        return Ok(cachorroExcluidoDTO);
    }

}
