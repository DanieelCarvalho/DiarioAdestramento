using DiarioAdestramento.Enums;

namespace DiarioAdestramento.Dtos;

public class CriarSessaoTreinoDto
{
    public int CachorroId { get; set; }
    public int LocalId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string OqueFoiTreinado { get; set; } = string.Empty;
    public string? RecompensasUtilizadas { get; set; }
    public TempoResposta TempoResposta { get; set; }
    public string? Observacoes { get; set; }
}