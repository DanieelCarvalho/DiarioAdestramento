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
            Data = sessao.Data,
            CachorroId = sessao.CachorroId,
            LocalId = sessao.LocalId,
            NomeCachorro = sessao.Cachorro?.Nome,
            NomeLocal = sessao.Local?.Nome,
            HoraInicio = sessao.HoraInicio,
            HoraFim = sessao.HoraFim,
            OqueFoiTreinado = sessao.OqueFoiTreinado,
            RecomepensasUtilizadas = sessao.RecomepensasUtilizadas,
            TempoResposta = sessao.TempoResposta,
            Obs = sessao.Obs,
            RegistrosClima = sessao.RegistrosClima,
           
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