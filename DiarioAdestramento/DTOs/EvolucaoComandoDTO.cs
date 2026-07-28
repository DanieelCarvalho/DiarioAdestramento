using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs.Estatisticas;

public class EvolucaoComandoDTO
{
    public DateTime Data { get; set; }
    public TempoResposta? TempoResposta { get; set; }
}