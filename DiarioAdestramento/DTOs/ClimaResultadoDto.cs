namespace DiarioAdestramento.DTOs;

public class ClimaResultadoDto
{
    public double TemperaturaCelsius { get; set; }
    public string CondicaoTempo { get; set; } = string.Empty;
    public double? Precipitacao { get; set; }
    public double? VelocidadeVento { get; set; }

}
