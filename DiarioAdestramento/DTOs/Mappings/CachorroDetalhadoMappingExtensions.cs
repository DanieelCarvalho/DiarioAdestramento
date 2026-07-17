using DiarioAdestramento.Models;

namespace DiarioAdestramento.DTOs.Mappings;

public static class CachorroDetalhadoMappingExtensions
{
    // Recebe o cachorro e a lista de sessões separadamente, porque agora
    // vêm de duas consultas diferentes (CachorroRepository + SessaoTreinoRepository),
    // e não mais de cachorro.Sessao diretamente.
    public static CachorroComSessoesResponseDTO? ToCachorroComSessoesResponseDTO(
        this Cachorro cachorro, IEnumerable<SessaoTreino> sessoes)
    {
        if (cachorro is null)
        {
            return null;
        }

        return new CachorroComSessoesResponseDTO
        {
            Id = cachorro.Id,
            Idade = cachorro.Idade,
            Nome = cachorro.Nome,
            Raca = cachorro.Raca,
            Sessoes = sessoes
                .Select(s => new SessaoResumoDTO
                {
                    Id = s.Id,
                    Data = s.Data,
                    HoraInicio = s.HoraInicio,
                    HoraFim = s.HoraFim,
                    OqueFoiTreinado = s.OqueFoiTreinado,
                    TempoResposta = s.TempoResposta,
                    Local = s.Local is null ? null : new LocalResumoDTO
                    {
                        Id = s.Local.Id,
                        Nome = s.Local.Nome
                    },
                    RegistrosClima = s.RegistrosClima?
                        .Select(r => new RegistroClimaResumoDTO
                        {
                            Momento = r.Momento,
                            TemperaturaCelsius = r.TemperaturaCelsius,
                            CondicaoTempo = r.CondicaoTempo
                        })
                        .ToList() ?? new List<RegistroClimaResumoDTO>()
                })
                .ToList()
        };
    }
}