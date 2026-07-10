using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services;

namespace DiarioAdestramento.Services;

public class SessaoTreinoService
{
    private readonly ISessaoTreinoRepository _sessaoRepository;
    private readonly ILocalRepository _localRepository;
    private readonly IClimaService _climaService;

    public SessaoTreinoService(ISessaoTreinoRepository sessaoRepository, 
                               ILocalRepository localRepository, 
                               IClimaService climaService)
    {
        _sessaoRepository = sessaoRepository;
        _localRepository = localRepository;
        _climaService = climaService;
    }

    public async Task<SessaoTreino> CriarSessaoAsync(SessaoTreino sessao)
    {
        var duracao = sessao.HoraFim - sessao.HoraInicio;
        if (duracao.TotalMinutes > 60)
            throw new InvalidOperationException("A sessão de treino não pode ultrapassar 60 minutos.");

        var local = await _localRepository.GetAsync(l => l.Id == sessao.LocalId);
        if (local is null)
            throw new InvalidOperationException("Local não encontrado.");

        var dataHoraInicio = sessao.Data.Date + sessao.HoraInicio;
        var climaInicio = await _climaService.ObterClimaHistoricoAsync(local.Latitude, local.Longitude, dataHoraInicio);
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
        var climaFim = await _climaService.ObterClimaHistoricoAsync(local.Latitude, local.Longitude, dataHoraFim);
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

        return await _sessaoRepository.AddAsync(sessao);
    }
}