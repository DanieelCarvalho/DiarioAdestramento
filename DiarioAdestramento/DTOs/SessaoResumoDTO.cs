using DiarioAdestramento.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiarioAdestramento.DTOs;

public class SessaoResumoDTO
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string OqueFoiTreinado { get; set; } = string.Empty;
    public TempoResposta? TempoResposta { get; set; }
    public LocalResumoDTO? Local { get; set; }

    public List<RegistroClimaResumoDTO> RegistrosClima { get; set; } = new();

    [NotMapped]

    public TimeSpan Duracao => HoraFim - HoraInicio;

}