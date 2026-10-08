using DiarioAdestramento.Dtos;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;

namespace DiarioAdestramento.Services;

public class SessaoTreinoService : ISessaoTreinoService
{
    private readonly ISessaoTreinoRepository _sessaoTreinoRepository;
    private readonly ICachorroRepository _cachorroRepository;
    private readonly ILocalRepository _localRepository;
    private readonly IClimaService _climaService;

    public SessaoTreinoService(
        ISessaoTreinoRepository sessaoTreinoRepository,
        ICachorroRepository cachorroRepository,
        ILocalRepository localRepository,
        IClimaService climaService)
    {
        _sessaoTreinoRepository = sessaoTreinoRepository;
        _cachorroRepository = cachorroRepository;
        _localRepository = localRepository;
        _climaService = climaService;
    }

    public async Task<(IEnumerable<SessaoTreinoResponseDTO> items, PaginationMetadata metadata)> GetAllSessoesAsync(
        SessoesParameters parametros, string adestradorId)
    {
        var sessoes = await _sessaoTreinoRepository.GetAllComDetalhesAsync(parametros, adestradorId);
        var sessoesDTO = sessoes.ToSessaoTreinoResponseDTOList();

        var metadata = new PaginationMetadata
        {
            TotalCount = sessoes.TotalCount,
            PageSize = sessoes.PageSize,
            CurrentPage = sessoes.CurrentPage,
            TotalPages = sessoes.TotalPages,
            HasNext = sessoes.HasNext,
            HasPrevious = sessoes.HasPrevious
        };

        return (sessoesDTO, metadata);
    }

    public async Task<SessaoTreinoResponseDTO?> GetSessaoByIdAsync(int id, string adestradorId)
    {
        var sessao = await _sessaoTreinoRepository.GetComDetalhesAsync(id, adestradorId);
        if (sessao is null)
            return null;

        if (sessao.Cachorro is null || sessao.Cachorro.AdestradorId != adestradorId)
            return null;

        return sessao.ToSessaoTreinoResponseDTO();
    }

    public async Task<(CachorroComSessoesResponseDTO? cachorro, PaginationMetadata? metadata)> GetSessoesByCachorroIdAsync(
        int cachorroId, SessoesParameters parametros, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == cachorroId && c.AdestradorId == adestradorId);
        if (cachorro is null)
            return (null, null);

        var sessoesPaginadas = await _sessaoTreinoRepository.GetPorCachorroAsync(
            cachorroId, adestradorId, parametros.PageNumber, parametros.PageSize);

        var metadata = new PaginationMetadata
        {
            TotalCount = sessoesPaginadas.TotalCount,
            PageSize = sessoesPaginadas.PageSize,
            CurrentPage = sessoesPaginadas.CurrentPage,
            TotalPages = sessoesPaginadas.TotalPages,
            HasNext = sessoesPaginadas.HasNext,
            HasPrevious = sessoesPaginadas.HasPrevious
        };

        var cachorroDTO = cachorro.ToCachorroComSessoesResponseDTO(sessoesPaginadas);
        return (cachorroDTO, metadata);
    }

    public async Task<(SessaoTreinoResponseDTO? sessao, string? erro)> CreateSessaoAsync(CriarSessaoTreinoDto dto, string adestradorId)
    {
        var duracao = dto.HoraFim - dto.HoraInicio;
        if (duracao.TotalMinutes <= 0 || duracao.TotalMinutes > 60)
            return (null, "A sessão de treino deve ter entre 1 e 60 minutos.");

        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == dto.CachorroId && c.AdestradorId == adestradorId);
        if (cachorro is null)
            return (null, "Cachorro inválido: não encontrado ou não pertence a este adestrador.");

        var local = await _localRepository.GetAsync(l => l.Id == dto.LocalId && l.AdestradorId == adestradorId);
        if (local is null)
            return (null, "Local informado não existe ou não pertence a este adestrador.");

        var sessao = dto.ToSessaoTreino();

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

        await _sessaoTreinoRepository.AddAsync(sessao);

        return (sessao.ToSessaoTreinoResponseDTO(), null);
    }

    public async Task<SessaoTreinoResponseDTO?> UpdateSessaoAsync(int id, UpdateSessaoTreinoDTO dto, string adestradorId)
    {
        var sessao = await _sessaoTreinoRepository.GetAsync(s => s.Id == id && s.Cachorro!.AdestradorId == adestradorId);
        if (sessao is null)
            return null;

        sessao.OqueFoiTreinado = dto.OqueFoiTreinado;
        sessao.RecomepensasUtilizadas = dto.RecompensasUtilizadas;
        sessao.TempoResposta = dto.TempoResposta;
        sessao.Obs = dto.Observacoes;

        var sessaoAtualizada = await _sessaoTreinoRepository.UpdateAsync(sessao);
        return sessaoAtualizada.ToSessaoTreinoResponseDTO();
    }

    public async Task<SessaoTreinoResponseDTO?> DeleteSessaoAsync(int id, string adestradorId)
    {
        var sessao = await _sessaoTreinoRepository.GetAsync(s => s.Id == id && s.Cachorro!.AdestradorId == adestradorId);
        if (sessao is null)
            return null;

        var sessaoExcluida = await _sessaoTreinoRepository.DeleteAsync(sessao);
        return sessaoExcluida.ToSessaoTreinoResponseDTO();
    }
}