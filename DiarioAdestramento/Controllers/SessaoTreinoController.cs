using DiarioAdestramento.Dtos;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiarioAdestramento.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SessaoTreinoController : ControllerBase
{
    private readonly ISessaoTreinoRepository _sessaoTreinoRepository;
    private readonly ILocalRepository _localRepository;

    public SessaoTreinoController(ISessaoTreinoRepository sessaoTreinoRepository, 
                                  ILocalRepository localRepository)
    {
        _sessaoTreinoRepository = sessaoTreinoRepository;
        _localRepository = localRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SessaoTreino>>> GetAll()
    {
        var sessoes = await _sessaoTreinoRepository.GetAllAsync();
        return Ok(sessoes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SessaoTreinoResponseDTO>> GetById(int id)
    {
        var sessao = await _sessaoTreinoRepository.GetComDetalhesAsync(id);
        if (sessao is null)
            return NotFound();

        return Ok(sessao);
    }

    [HttpPost]
    public async Task<ActionResult<SessaoTreino>> Create(CriarSessaoTreinoDto dto)
    {
        var duracao = dto.HoraFim - dto.HoraInicio;
        if (duracao.TotalMinutes <= 0 || duracao.TotalMinutes > 60)
            return BadRequest("A sessão de treino deve ter entre 1 e 60 minutos.");

        var local = await _localRepository.GetAsync(l => l.Id == dto.LocalId);
        if (local is null)
            return BadRequest("Local informado não existe.");

        var sessao = new SessaoTreino
        {
            CachorroId = dto.CachorroId,
            LocalId = dto.LocalId,
            Data = dto.Data,
            HoraInicio = dto.HoraInicio,
            HoraFim = dto.HoraFim,
            OqueFoiTreinado = dto.OqueFoiTreinado,
            RecomepensasUtilizadas = dto.RecompensasUtilizadas,
            TempoResposta = dto.TempoResposta,
            Obs = dto.Observacoes
        };

        // É aqui que o clima do início e do fim são buscados e anexados,
        // antes da sessão ser persistida no banco.
        var sessaoCriada = await _sessaoTreinoRepository.CriarComClimaAsync(sessao, local);

        return CreatedAtAction(nameof(GetById), new { id = sessaoCriada.Id }, sessaoCriada);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<SessaoTreino>> Update(int id, UpdateSessaoTreinoDTO dto)
    {
        var sessao = await _sessaoTreinoRepository.GetAsync(s => s.Id == id);
        if (sessao is null)
            return NotFound();

        // Só os campos "de texto" são editáveis — ver comentário no AtualizarSessaoTreinoDto
        sessao.OqueFoiTreinado = dto.OqueFoiTreinado;
        sessao.RecomepensasUtilizadas = dto.RecompensasUtilizadas;
        sessao.TempoResposta = dto.TempoResposta;
        sessao.Obs = dto.Observacoes;

        var sessaoAtualizada = await _sessaoTreinoRepository.UpdateAsync(sessao);
        return Ok(sessaoAtualizada);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sessao = await _sessaoTreinoRepository.GetAsync(s => s.Id == id);
        if (sessao is null)
            return NotFound();

        await _sessaoTreinoRepository.DeleteAsync(sessao);
        return NoContent();
    }

}
