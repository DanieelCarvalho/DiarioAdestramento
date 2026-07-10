using DiarioAdestramento.Context;
using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Repositories;

public class SessaoTreinoRepository : Repository<SessaoTreino>, ISessaoTreinoRepository
{
    private readonly IClimaService _climaService;
    private readonly AppDbContext _contextRepository;
    public SessaoTreinoRepository(AppDbContext context, IClimaService climaService, AppDbContext contextRepository) 
        : base(context)
    {
        _climaService = climaService;
        _contextRepository = contextRepository;
    }

    public async Task<SessaoTreino> CriarComClimaAsync(SessaoTreino sessao, Local local)
    {
        // Busca o clima no início da sessão
        var dataHoraInicio = sessao.Data.Date + sessao.HoraInicio;
        var climaInicio = await _climaService.ObterClimaHistoricoAsync(
            local.Latitude, local.Longitude, dataHoraInicio);

        if (climaInicio is not null)
        {
            sessao.RegistrosClima.Add(new RegistroClima
            {
                Momento = MomentoClima.Inicio,
                TemperaturaCelsius = climaInicio.TemperaturaCelsius,
                CondicaoTempo = climaInicio.CondicaoTempo,
                Precipitacao = climaInicio.Precipitacao,
                VelocidadeDeVento = climaInicio.VelocidadeVento
            });
        }

     
        var dataHoraFim = sessao.Data.Date + sessao.HoraFim;
        var climaFim = await _climaService.ObterClimaHistoricoAsync(
            local.Latitude, local.Longitude, dataHoraFim);

        if (climaFim is not null)
        {
            sessao.RegistrosClima.Add(new RegistroClima
            {
                Momento = MomentoClima.Fim,
                TemperaturaCelsius = climaFim.TemperaturaCelsius,
                CondicaoTempo = climaFim.CondicaoTempo,
                Precipitacao = climaFim.Precipitacao,
                VelocidadeDeVento = climaFim.VelocidadeVento
            });
        }

      
        return await AddAsync(sessao);
    }

   

    public async Task<SessaoTreino?> GetComDetalhesAsync(int id)
    {
       return await _contextRepository.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .Include(s => s.RegistrosClima)
            .Include(s => s.Local)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
