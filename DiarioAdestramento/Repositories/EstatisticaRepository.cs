using DiarioAdestramento.Context;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Estatisticas;
using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Repositories;

public class EstatisticaRepository : IEstatisticaRepository
{
    private readonly AppDbContext _context;

    public EstatisticaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DesempenhoPorClimaDTO>> GetDesempenhoPorClimaAsync(int cachorroId)
    {
        var resultado = await _context.RegistrosClima
            .Where(r => r.Momento == MomentoClima.Inicio && r.Sessao.CachorroId == cachorroId)
            .GroupBy(r => r.CondicaoTempo)
            .Select(g => new DesempenhoPorClimaDTO
            {
                Condicao = g.Key,
                TotalSessoes = g.Count(),
                RespostasExcelentes = g.Count(r => r.Sessao.TempoResposta == TempoResposta.Excelente),
                RespostasBoas = g.Count(r => r.Sessao.TempoResposta == TempoResposta.Bom),
                RespostasRegulares  = g.Count(r => r.Sessao.TempoResposta == TempoResposta.Regular),
                RespostasRuins = g.Count(r => r.Sessao.TempoResposta == TempoResposta.Ruim),
                TemperaturaMedia = g.Average(r => r.TemperaturaCelsius)
            })
            .ToListAsync();

        return resultado;
    }

    public async Task<IEnumerable<DesempenhoPorLocalDTO>> GetDesempenhoPorLocalAsync(int cachorroId)
    {
        return await _context.Set<SessaoTreino>()
            .Where(s => s.CachorroId == cachorroId)
            .GroupBy(s => new { s.Local!.Nome, s.Local!.TipoDoLocal })
            .Select(g => new DesempenhoPorLocalDTO
            {
                NomeLocal = g.Key.Nome,
                TipoDoLocal = g.Key.TipoDoLocal,
                TotalSessoes = g.Count(),
                PercentualExcelente = g.Count(s => s.TempoResposta == TempoResposta.Excelente) * 100.0 / g.Count(),
                PercentualBom = g.Count(s => s.TempoResposta == TempoResposta.Bom) * 100.0 / g.Count(),
                PercentualRegular = g.Count(s => s.TempoResposta == TempoResposta.Regular) * 100.0 / g.Count(),
                PercentualRuim = g.Count(s => s.TempoResposta == TempoResposta.Ruim) * 100.0 / g.Count(),


            })
            .ToListAsync();
    }


    public async Task<IEnumerable<EvolucaoComandoDTO>> GetEvolucaoPorComandoAsync(int cachorroId, string comando)
    {
        return await _context.Set<SessaoTreino>()
            .Where(s => s.CachorroId == cachorroId && s.OqueFoiTreinado == comando)
            .OrderBy(s => s.Data)
            .Select(s => new EvolucaoComandoDTO
            {
                Data = s.Data,
                TempoResposta = s.TempoResposta
            })
            .ToListAsync();
    }

}
