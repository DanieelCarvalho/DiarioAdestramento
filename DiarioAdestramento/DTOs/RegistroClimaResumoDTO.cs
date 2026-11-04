using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs;

public class RegistroClimaResumoDTO
{
  
    public double TemperaturaCelsius { get; set; }
    public string? CondicaoTempo { get; set; }
    public double? Precipitacao { get; set; }
    public double? VelocidadeDeVento { get; set; }
}
