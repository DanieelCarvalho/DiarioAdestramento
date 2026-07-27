using DiarioAdestramento.Context;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Enums;
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

    public Task<IEnumerable<EvolucaoComandoDTO>> GetEvolucaoPorComandoAsync(int cachorroId, string comando)
    {
        throw new NotImplementedException();
    }
}
