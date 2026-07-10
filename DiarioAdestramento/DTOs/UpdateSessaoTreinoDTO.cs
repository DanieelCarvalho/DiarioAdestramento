using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs;

public class UpdateSessaoTreinoDTO
{
    public string OqueFoiTreinado { get; set; } = string.Empty;
    public string? RecompensasUtilizadas { get; set; }
    public TempoResposta TempoResposta { get; set; }
    public string? Observacoes { get; set; }

}
