using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiarioAdestramento.DTOs;

public class SessaoTreinoResponseDTO
{
    public int Id { get; set; }
    public int CachorroId { get; set; }
    public string? NomeCachorro { get; set; }
    public string? NomeLocal { get; set; }
    public int LocalId { get; set; }
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string? OqueFoiTreinado { get; set; }
    public string? RecomepensasUtilizadas { get; set; }
    public TempoResposta? TempoResposta { get; set; }
    public string? Obs { get; set; }
    public RegistroClimaResumoDTO? RegistroClima { get; set; } 
  
    [NotMapped]
    public TimeSpan Duracao => HoraFim - HoraInicio;
}
