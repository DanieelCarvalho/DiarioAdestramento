using DiarioAdestramento.Enums;
using System.ComponentModel.DataAnnotations;

namespace DiarioAdestramento.Dtos;

public class CriarSessaoTreinoDto
{
    public int CachorroId { get; set; }

    [Required(ErrorMessage = "O campo LocalId é obrigatório.")]
    public int LocalId { get; set; }
    [Required(ErrorMessage = "O campo Data é obrigatório.")]
    public DateTime Data { get; set; }
    [Required(ErrorMessage = "O campo HoraInicio é obrigatório.")]
    public TimeSpan HoraInicio { get; set; }
    [Required(ErrorMessage = "O campo HoraFim é obrigatório.")]
    public TimeSpan HoraFim { get; set; }
    [Required(ErrorMessage = "O campo OqueFoiTreinado é obrigatório.")]
    public string OqueFoiTreinado { get; set; } = string.Empty;
    public string? RecompensasUtilizadas { get; set; }
    [Required(ErrorMessage = "O campo TempoResposta é obrigatório.")]

    /// <summary>Tempo de resposta do cão: 0 = Excelente, 1 = Bom, 2 = Regular, 3 = Ruim.</summary>
    public TempoResposta TempoResposta { get; set; }
    public string? Observacoes { get; set; }
}