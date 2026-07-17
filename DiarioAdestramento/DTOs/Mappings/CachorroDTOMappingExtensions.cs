using DiarioAdestramento.Models;

namespace DiarioAdestramento.DTOs.Mappings;

public static  class CachorroDTOMappingExtensions
{
    public static CachorroResponseDTO ToCachorroResponseDTO(this Cachorro cachorro)
    {
        if(cachorro is null)
        {
            return null;
        }

        return new CachorroResponseDTO
        {
            Id = cachorro.Id,
            Idade = cachorro.Idade,
            Nome = cachorro.Nome,
            Raca = cachorro.Raca,

            
        };
    }

    public static CachorroComSessoesResponseDTO? ToCachorroComSessoesResponseDTO(this Cachorro cachorro)
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
            Sessoes = cachorro.Sessao?
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
                    }
                    
                })
                .ToList() ?? new List<SessaoResumoDTO>()
        };
    }

    public static Cachorro ToCachorro(this CachorroCreatedDTO cachorroCreatedDTO)
    {
        if(cachorroCreatedDTO is null)
        {
            return null;
        }

        return new Cachorro
        {
            Id = cachorroCreatedDTO.Id,
            Idade = cachorroCreatedDTO.Idade,
            Nome = cachorroCreatedDTO.Nome,
            Raca = cachorroCreatedDTO.Raca,
           
            
        };
    }

    public static IEnumerable<CachorroResponseDTO> ToCachorroResponseDTOList(this IEnumerable<Cachorro> cachorros)
    {
        if(cachorros is null || !cachorros.Any())
        {
            return new List<CachorroResponseDTO>();
        }
        return cachorros.Select(c => c.ToCachorroResponseDTO()).ToList();
    }

}
