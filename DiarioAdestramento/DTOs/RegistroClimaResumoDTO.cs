using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs;

public class RegistroClimaResumoDTO
{
    public MomentoClima? Momento { get; set; }
    public double TemperaturaCelsius { get; set; }
    public string? CondicaoTempo { get; set; }
}
