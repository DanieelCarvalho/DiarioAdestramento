using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs;

public class SessaoListagemDTO
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string OqueFoiTreinado { get; set; } = string.Empty;
    public TempoResposta? TempoResposta { get; set; }
    public string? NomeCachorro { get; set; }
    public string? NomeLocal { get; set; }
}