using DiarioAdestramento.Dtos;
using DiarioAdestramento.Models;

namespace DiarioAdestramento.DTOs.Mappings;

public static class SessaoTreinoDTOMappingExtensions
{
    public static SessaoTreinoResponseDTO? ToSessaoTreinoResponseDTO(this SessaoTreino sessao)
    {
        if (sessao is null)
        {
            return null;
        }

        return new SessaoTreinoResponseDTO
        {
            Id = sessao.Id,
            CachorroId = sessao.CachorroId,
            LocalId = sessao.LocalId,
            Cachorro = sessao.Cachorro is null ? null : new CachorroResumoDTO
            {
                Id = sessao.Cachorro.Id,
                Nome = sessao.Cachorro.Nome
            },
            Local = sessao.Local is null ? null : new LocalResumoDTO
            {
                Id = sessao.Local.Id,
                Nome = sessao.Local.Name
            },
            Data = sessao.Data,
            HoraInicio = sessao.HoraInicio,
            HoraFim = sessao.HoraFim,
            OqueFoiTreinado = sessao.OqueFoiTreinado,
            RecomepensasUtilizadas = sessao.RecomepensasUtilizadas,
            TempoResposta = sessao.TempoResposta,
            Obs = sessao.Obs,
            RegistrosClima = sessao.RegistrosClima
        };
    }

    public static SessaoTreino? ToSessaoTreino(this CriarSessaoTreinoDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new SessaoTreino
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
    }

    public static SessaoListagemDTO? ToSessaoListagemDTO(this SessaoTreino sessao)
    {
        if (sessao is null)
        {
            return null;
        }

        return new SessaoListagemDTO
        {
            Id = sessao.Id,
            Data = sessao.Data,
            HoraInicio = sessao.HoraInicio,
            HoraFim = sessao.HoraFim,
            OqueFoiTreinado = sessao.OqueFoiTreinado,
            TempoResposta = sessao.TempoResposta,
            NomeCachorro = sessao.Cachorro?.Nome,
            NomeLocal = sessao.Local?.Name
        };
    }

    public static IEnumerable<SessaoListagemDTO> ToSessaoListagemDTOList(this IEnumerable<SessaoTreino> sessoes)
    {
        if (sessoes is null || !sessoes.Any())
        {
            return new List<SessaoListagemDTO>();
        }

        return sessoes
            .Select(s => s.ToSessaoListagemDTO())
            .Where(dto => dto is not null)
            .Select(dto => dto!)
            .ToList();
    }

    public static IEnumerable<SessaoTreinoResponseDTO> ToSessaoTreinoResponseDTOList(this IEnumerable<SessaoTreino> sessoes)
    {
        if (sessoes is null || !sessoes.Any())
        {
            return new List<SessaoTreinoResponseDTO>();
        }

        return sessoes
            .Select(s => s.ToSessaoTreinoResponseDTO())
            .Where(dto => dto is not null)
            .Select(dto => dto!)
            .ToList();
    }
}