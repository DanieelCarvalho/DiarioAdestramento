using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiarioAdestramento.DTOs;

public class SessaoTreinoResumidaDTO
{
    public int Id { get; set; }
    public int LocalId { get; set; }
    public string? NomeLocal { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string? OqueFoiTreinado { get; set; }
    public string? RecomepensasUtilizadas { get; set; }
    public TempoResposta? TempoResposta { get; set; }
    public string? Obs { get; set; }
    public ICollection<RegistroClima>? RegistrosClima { get; set; }

    [NotMapped]
    public TimeSpan Duracao => HoraFim - HoraInicio;
}
